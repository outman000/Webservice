using Eday.Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eday.Webservice.BuildingBlocks.EventBus.Abstractions
{
    public  interface IEventBus
    {
        /// <summary>
        ///  事件总线会向订阅该事件的任何微服务或外部应用程序，广播经过它的集成事件。 该方法由发布事件的微服务使用
        /// </summary>
        /// <param name="event"></param>
        void Publish(IntegrationEvent @event);

        /// <summary>
        /// Subscribe 方法（你可能有多个实现，具体取决于参数）由要接收事件的微服务使用。 
        /// 此方法具有两个参数。 第一个是要订阅的集成事件 (IntegrationEvent)。
        /// 第二个参数是名为 IIntegrationEventHandler<T> 的集成事件处理程序（或回调方法），
        /// 用于在接收者微服务获得集成事件消息时执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
