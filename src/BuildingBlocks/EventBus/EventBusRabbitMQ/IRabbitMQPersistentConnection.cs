using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebServers.BuildingBlocks.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
       : IDisposable
    {
        /// <summary>
        /// 是否连接
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// 尝试连接
        /// </summary>
        /// <returns></returns>
        bool TryConnect();
        /// <summary>
        /// 创建生产者
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}
