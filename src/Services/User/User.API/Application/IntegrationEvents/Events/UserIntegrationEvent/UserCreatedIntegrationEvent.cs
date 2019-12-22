using Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Application.IntegrationEvents.Events.UserIntegrationEvent
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public UserCreatedIntegrationEvent (string userId) => UserId = userId;
    }
}
