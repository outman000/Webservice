using Webservice.BuildingBlocks.EventBus.Events;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        /// <summary>
        ///  异步保存事件
        /// </summary>
        /// <param name="event"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
        /// <summary>
        /// 标记事件已发布
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkEventAsPublishedAsync(Guid eventId);
        /// <summary>
        /// 标记时间再进行中
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkEventAsInProgressAsync(Guid eventId);
        /// <summary>
        /// 标记事件失败
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
