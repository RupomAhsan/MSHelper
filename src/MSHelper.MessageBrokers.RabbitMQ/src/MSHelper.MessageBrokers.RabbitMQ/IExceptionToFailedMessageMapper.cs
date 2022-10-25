using System;

namespace MSHelper.MessageBrokers.RabbitMQ;

public interface IExceptionToFailedMessageMapper
{
    FailedMessage Map(Exception exception, object message);
}