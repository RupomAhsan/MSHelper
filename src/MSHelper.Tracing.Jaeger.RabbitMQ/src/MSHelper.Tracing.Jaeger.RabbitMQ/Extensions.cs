using MSHelper.MessageBrokers.RabbitMQ;
using MSHelper.Tracing.Jaeger.RabbitMQ.Plugins;

namespace MSHelper.Tracing.Jaeger.RabbitMQ;

public static class Extensions
{
    public static IRabbitMqPluginsRegistry AddJaegerRabbitMqPlugin(this IRabbitMqPluginsRegistry registry)
    {
        registry.Add<JaegerPlugin>();
        return registry;
    }
}