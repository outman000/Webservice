using Eday.Webservice.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Application.IntegrationEvents.Events.UserIntegrationEvent
{
    public class UserStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public UserStartedIntegrationEvent(string userId)
            => UserId = userId;
    }
}
