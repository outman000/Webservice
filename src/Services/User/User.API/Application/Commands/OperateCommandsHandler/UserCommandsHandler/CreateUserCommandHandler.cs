﻿using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using User.API.Application.Commands.OperateCommands.UserCommands;
using User.API.Application.IntegrationEvents;
using User.API.Application.IntegrationEvents.Events.UserIntegrationEvent;
using User.API.Infrastructure.Services;
using User.Domain.AggregatesModel.UserAggregates.Entitys;
using User.Domain.AggregatesModel.UserAggregates.Respository;

namespace User.API.Application.Commands.OperateCommandsHandler.UserCommandsHandler
{
  
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {

        private readonly IUserRespository _userRespository;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IUserIntegrationEventService _userIntegrationEventService;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRespository userRespository, IIdentityService identityService, IMediator mediator, ILogger<CreateUserCommandHandler> logger)
        {
            _userRespository = userRespository ?? throw new ArgumentNullException(nameof(userRespository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(CreateUserCommand message, CancellationToken cancellationToken)
        {
            //添加一个事件到一个 新篮子里面
            var userCreatedIntegrationEvent = new UserCreatedIntegrationEvent(message.UserId);
            //添加然后保存事件
            //这里应该就回去找他的事件处理程序
            await _userIntegrationEventService.AddAndSaveEventAsync(userCreatedIntegrationEvent);

            ///  添加 / 更新 用户聚合根
            //  DDD 模式注释：通过顺序聚合根添加子实体和值对象
            //   方法和构造函数，以便验证、不变量和业务逻辑
            ///  确保在整个聚合中保持一致性

            UserInformation userinfo = null;
            _logger.LogInformation("----- Creating Order - Order: {@Order}", userinfo);
 
            //添加
            _userRespository.Add(userinfo);

            return await _userRespository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }

    }
}
