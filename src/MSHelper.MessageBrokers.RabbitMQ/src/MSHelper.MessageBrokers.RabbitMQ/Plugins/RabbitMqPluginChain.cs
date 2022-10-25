using System;

namespace MSHelper.MessageBrokers.RabbitMQ.Plugins;

internal sealed class RabbitMqPluginChain
{
    public Type PluginType { get; set; }
}