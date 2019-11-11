using Eday.Webservice.BuildingBlocks.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Application.IntegrationEvents.Events.UserIntegrationEvent;

namespace User.API.Application.IntegrationEvents.EventHandling.UserIntegrationHanding
{
    public class UserStartedIntegrationEventHandler : IIntegrationEventHandler<UserStartedIntegrationEvent>
    {
        public Task Handle(UserStartedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
