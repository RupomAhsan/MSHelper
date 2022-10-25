using System;

namespace MSHelper.MessageBrokers.RabbitMQ;

public interface IConventionsBuilder
{
    string GetRoutingKey(Type type);
    string GetExchange(Type type);
    string GetQueue(Type type);
}