using System.Collections.Generic;

namespace MSHelper.MessageBrokers.RabbitMQ.Plugins;

internal interface IRabbitMqPluginsRegistryAccessor
{
    LinkedList<RabbitMqPluginChain> Get();
}