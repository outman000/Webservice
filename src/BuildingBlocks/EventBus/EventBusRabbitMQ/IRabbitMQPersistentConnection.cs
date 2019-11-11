using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eday.WebServers.BuildingBlocks.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
       : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
