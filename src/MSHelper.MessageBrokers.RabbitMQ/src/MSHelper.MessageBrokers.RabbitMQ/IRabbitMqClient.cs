using System.Collections.Generic;

namespace MSHelper.MessageBrokers.RabbitMQ;

public interface IRabbitMqClient
{
    void Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
        string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null);
}