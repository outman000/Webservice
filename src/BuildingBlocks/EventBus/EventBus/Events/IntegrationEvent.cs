using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webservice.BuildingBlocks.EventBus.Events
{
    /// <summary>
    /// 发布的内容（消息）必须是IntegrationEvent及其子类
    /// </summary>
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
