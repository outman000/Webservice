using Autofac;
using Webservice.BuildingBlocks.EventBus;
using Webservice.BuildingBlocks.EventBus.Abstractions;
using Webservice.BuildingBlocks.EventBus.Events;
using Webservice.BuildingBlocks.EventBus.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServers.BuildingBlocks.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        //交换机名称
        const string BROKER_NAME = "User_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        private readonly string AUTOFAC_SCOPE_NAME = "eshop_event_bus";
        private readonly int _retryCount;

        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger,
            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager, string queueName = null, int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = queueName; //队列名称
            _consumerChannel = CreateConsumerChannel();//消费者
            _autofac = autofac;
            _retryCount = retryCount;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            //如果没有请求到连接,尝试重连
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            //创建频道
            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);
                //如果订阅是空的,频道边为空
                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }
        //发布消息
        public void Publish(IntegrationEvent @event)
        {
            //判断是否连接
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            //如果有异常，重连，还是不行，将事件记录到日志当中
            //使用Polly，以2的阶乘的时间间隔进行重试。（第一次2s后，第二次4s后，第三次8s后...重试）
            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            //首先传见一个频道
            using (var channel = _persistentConnection.CreateModel())
            {
                //记录申明
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);
                //定义一个交换机
                //使用direct全匹配、单播形式的路由机制进行消息分发
                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // 消息持久化

                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);
                    //发布消息
                    channel.BasicPublish(
                        exchange: BROKER_NAME,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
            StartBasicConsume();
        }
        /// <summary>
        /// 事件订阅
        /// </summary>
        /// <typeparam name="T">事件</typeparam>
        /// <typeparam name="TH">事件处理接口</typeparam>
        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            //获取事件名
            var eventName = _subsManager.GetEventKey<T>();
            //向mq添加注册
            DoInternalSubscription(eventName);

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            _subsManager.AddSubscription<T, TH>();
            StartBasicConsume();
        }
        //事件订阅逻辑
        private void DoInternalSubscription(string eventName)
        {
            //检查当前事件是否存在
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                                      exchange: BROKER_NAME,
                                      routingKey: eventName);
                }
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        //销毁
        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }



        /// <summary>
        /// 创建消费者
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME,
                                    type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            //回调出现异常
            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        /// <summary>
        /// 根据队列 开始消费
        /// </summary>
        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                //事件基本消费者
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
                //接收到消息后触发Consumer_Received事件
                consumer.Received += Consumer_Received;//妙啊

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }
        /// <summary>
        /// 消费者接收到消息的时候触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;//获取事件名称
            var message = Encoding.UTF8.GetString(eventArgs.Body);//获取消息

            try
            {
                //如果包含异常信息那么直接抛出异常
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }
                //执行处理程序
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }

            //  即使在例外情况下，消息从队列中取下。
            //在真实世界应用程序中，这应该使用死信交换 （DLX） 来处理。   
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
            //先判断有没有这个事件
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    //获取事件的所有处理程序
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    //循环所有的处理程序
                    //将eventname转为类型
                    //将eventname类型赋值给处理程序的泛型
                    //执行处理程序的handle方法
                    foreach (var subscription in subscriptions)
                    {
                        //如果是Dynamic
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;
                            dynamic eventData = JObject.Parse(message);

                            await Task.Yield();
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            //从容器中解析这个服务的实例
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            //如果没有继续
                            if (handler == null) continue;
                            //获取事件类型
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            //将message序列化为一个引用类型
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            //将eventType分配给IIntegrationEventHandler（处理程序）泛型
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await Task.Yield();
                            //通过反射执行处理程序的handle方法
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
            }
        }



    }
}
