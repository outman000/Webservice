using Webservice.BuildingBlocks.EventBus.Abstractions;
using Webservice.BuildingBlocks.EventBus.Events;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using User.Infrastructure;

namespace User.API.Application.IntegrationEvents
{
    //用户集成事件处理程序
    public class UserIntegrationEventService: IUserIntegrationEventService
    {

        //根据数据库连接字符串，返回一个集成事件工厂
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly UserContext _userContext;
        private readonly IntegrationEventLogContext _eventLogContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<UserIntegrationEventService> _logger;

        public UserIntegrationEventService(IEventBus eventBus,
            UserContext  userContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<UserIntegrationEventService> logger)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_userContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        //通过总线发布时间
        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

                try
                {
                    //标记进行中
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    //时间发布
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    //标记结束
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    //异常往外走
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);
                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }
        //添加然后保存事件
        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _userContext.GetCurrentTransaction());
        }
     
 

    }
}
