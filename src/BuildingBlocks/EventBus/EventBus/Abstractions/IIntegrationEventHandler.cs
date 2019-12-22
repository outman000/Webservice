using Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Webservice.BuildingBlocks.EventBus.Abstractions
{
    /// <summary>
    /// 处理器必须是IIntegrationEventHandler的实现类
    /// 他的泛型就是他的具体事件，处理程序必须处理实现handle方法
    /// </summary>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
      where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
