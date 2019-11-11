using Eday.Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Application.IntegrationEvents
{
    interface IUserIntegrationEventService
    {
        /// <summary>
        /// 通过命令总线发布命令
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
