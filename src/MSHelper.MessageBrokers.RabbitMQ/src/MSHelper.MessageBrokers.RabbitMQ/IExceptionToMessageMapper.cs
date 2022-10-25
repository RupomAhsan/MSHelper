using System;

namespace MSHelper.MessageBrokers.RabbitMQ;

public interface IExceptionToMessageMapper
{
    object Map(Exception exception, object message);
}