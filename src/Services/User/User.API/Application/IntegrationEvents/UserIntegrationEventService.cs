﻿using Eday.Webservice.BuildingBlocks.EventBus.Events;
using IntegrationEventLogEF.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Infrastructure;

namespace User.API.Application.IntegrationEvents
{
    public class UserIntegrationEventService
    {


        //private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        //private readonly IEventBus _eventBus;
        private readonly UserContext userContext;
        //private readonly IntegrationEventLogContext _eventLogContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<UserIntegrationEventService> _logger;

        //public OrderingIntegrationEventService(IEventBus eventBus,
        //    OrderingContext orderingContext,
        //    IntegrationEventLogContext eventLogContext,
        //    Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
        //    ILogger<OrderingIntegrationEventService> logger)
        //{
        //    _orderingContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
        //    _eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
        //    _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
        //    _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        //    _eventLogService = _integrationEventLogServiceFactory(_orderingContext.Database.GetDbConnection());
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        //}

        //public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        //{
        //    var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        //    foreach (var logEvt in pendingLogEvents)
        //    {
        //        _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

        //        try
        //        {
        //            await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
        //            _eventBus.Publish(logEvt.IntegrationEvent);
        //            await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);

        //            await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
        //        }
        //    }
        //}

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, userContext.GetCurrentTransaction());
        }

    }
}
