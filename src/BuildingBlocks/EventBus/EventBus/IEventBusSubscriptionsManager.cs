using Webservice.BuildingBlocks.EventBus.Abstractions;
using Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webservice.BuildingBlocks.EventBus
{
    /// <summary>
    /// 事件总线订阅管理接口（订阅管理器接口）
    /// </summary>
    public interface IEventBusSubscriptionsManager
    {

        bool IsEmpty { get; }
        //定义一个事件类型
        event EventHandler<string> OnEventRemoved;
        /// <summary>
        /// 添加动态订阅事件
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 添加订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;
        /// <summary>
        /// 删除订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;
        /// <summary>
        /// 删除动态事件
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 判断是否存在某种订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        /// <summary>
        /// 根据事件名称判断是否存在某种事件
        /// </summary>
        /// <param name="eventName"></param>
        bool HasSubscriptionsForEvent(string eventName);
        /// <summary>
        /// 获取事件类型
        /// </summary>
        /// <param name="eventName"></param>
        Type GetEventTypeByName(string eventName);
        /// <summary>
        /// 清除事件
        /// </summary>
        void Clear();
        /// <summary>
        /// 获取订阅事件集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        /// <summary>
        /// 获取订阅时间集合
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        /// <summary>
        /// 获取事件标识
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetEventKey<T>();
    }
}
