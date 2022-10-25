using System;
using System.Threading.Tasks;

namespace MSHelper.MessageBrokers.RabbitMQ.Subscribers;

internal interface IMessageSubscriber
{
    MessageSubscriberAction Action { get; }
    Type Type { get; }
    Func<IServiceProvider, object, object, Task> Handle { get; }
}