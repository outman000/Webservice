using Eday.Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eday.Webservice.BuildingBlocks.EventBus.Abstractions
{
    /// <summary>
    /// 处理器必须是IIntegrationEventHandler的实现类
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
