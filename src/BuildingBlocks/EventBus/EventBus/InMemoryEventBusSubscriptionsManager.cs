using Eday.Webservice.BuildingBlocks.EventBus.Abstractions;
using Eday.Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eday.Webservice.BuildingBlocks.EventBus
{
    /// <summary>
    /// 基于内存的事件总线实现方法
    /// </summary>
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    
    {
        //事件信息集合（一个事件对应着多个订阅）
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        //事件类型集合
        private readonly List<Type> _eventTypes;
        //事件变量
        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }
        //集合非空判断
        public bool IsEmpty => !_handlers.Keys.Any();
        //清除集合元素
        public void Clear() => _handlers.Clear();
        /// <summary>
        /// 添加一个动态订阅
        /// </summary>
        /// <typeparam name="TH">处理程序类型</typeparam>
        /// <param name="eventName">事件名称</param>
        public void AddDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            DoAddSubscription(typeof(TH), eventName, isDynamic: true);
        }
        /// <summary>
        /// 添加一个订阅
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <typeparam name="TH"></typeparam>
        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            DoAddSubscription(typeof(TH), eventName, isDynamic: false);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }
       /// <summary>
       /// 给事件添加一个订阅
       /// </summary>
       /// <param name="handlerType">处理程序类型</param>
       /// <param name="eventName">事件名称</param>
       /// <param name="isDynamic">事件类型</param>
        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            //判断事件是否有订阅
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }
            //如果事件存在处理程序，那么直接抛出异常，
            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }
            //判断事件配型，给事件添加订阅信息
            if (isDynamic)
            {
                _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }
        }

        /// <summary>
        /// 根据根据事件名称删除其动态订阅
        /// </summary>
        /// <typeparam name="TH">处理程序</typeparam>
        /// <param name="eventName"></param>
        public void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            var handlerToRemove = FindDynamicSubscriptionToRemove<TH>(eventName);
            DoRemoveHandler(eventName, handlerToRemove);
        }
        
        /// <summary>
        /// 根据事件类型和处理程序类型从事件集合中删除
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <typeparam name="TH">处理程序类型</typeparam>
        public void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            DoRemoveHandler(eventName, handlerToRemove);
        }

        /// <summary>
        /// 根据事件以及处理程序
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="subsToRemove"></param>
        private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
        {
            if (subsToRemove != null)
            {
                _handlers[eventName].Remove(subsToRemove);
                //判断事件是否存在处理程序
                if (!_handlers[eventName].Any())
                {
                    //从集合中删除处理程序
                    _handlers.Remove(eventName);
                    //查询事件名称
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        //从事件类型中删除
                        _eventTypes.Remove(eventType);
                    }
                    //触发事件删除后的事件
                    RaiseOnEventRemoved(eventName);
                }

            }
        }
       /// <summary>
       /// 获取某一类型的事件的处理程序
       /// </summary>
       /// <typeparam name="T">事件类型</typeparam>
       /// <returns></returns>
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }
        /// <summary>
        /// 根据事件名称查询其处理程序
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

       /// <summary>
       /// 通过事件名称查找需要删除的订阅，事件处理程序通过泛型传入
       /// </summary>
       /// <typeparam name="TH">处理程序类型</typeparam>
       /// <param name="eventName">事件名称</param>
       /// <returns></returns>
        private SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }
        /// <summary>
        /// 根据事件类型和事件处理类型来查找订阅
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <typeparam name="TH">事件处理程序类型</typeparam>
        /// <returns></returns>
        private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
             where T : IntegrationEvent
             where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }
       /// <summary>
       /// 通过事件以及其处理程序查找订阅
       /// </summary>
       /// <param name="eventName">事件名称</param>
       /// <param name="handlerType">处理程序类型</param>
       /// <returns></returns>
        private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);

        }
        /// <summary>
        /// 确认某一事件类型是否存在订阅
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }
        /// <summary>
        /// 确认某一事件是否存在订阅
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);
        /// <summary>
        /// 根据事件名称查询他的事件类型
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }
    }
}
