using RabbitMQ.Client;

namespace MSHelper.MessageBrokers.RabbitMQ;

public sealed class ProducerConnection
{
    public IConnection Connection { get; }

    public ProducerConnection(IConnection connection)
    {
        Connection = connection;
    }
}