using Webservice.BuildingBlocks.EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Context;
using User.API.Application.IntegrationEvents.Events.UserIntegrationEvent;
using User.API.Application.Commands.OperateCommands.UserCommands;
using Webservice.BuildingBlocks.EventBus.Extensions;

namespace User.API.Application.IntegrationEvents.EventHandling.UserIntegrationHanding
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;

        public UserCreatedIntegrationEventHandler(IMediator mediator, ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            //Serilog的Sql Server配置的自定义字段部分,发生事件记录在数据库
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                //记录事件处理程序启动
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                //这里写需要执行的命令
                var command = new CreateUserCommand(@event.UserId);
                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    command.GetGenericTypeName(),
                    nameof(command.UserId),
                    command.UserId,
                    command);

                await _mediator.Send(command);
            }
        }
    }
}
