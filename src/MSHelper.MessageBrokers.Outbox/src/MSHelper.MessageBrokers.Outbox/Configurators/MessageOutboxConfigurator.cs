namespace MSHelper.MessageBrokers.Outbox.Configurators;

internal sealed class MessageOutboxConfigurator : IMessageOutboxConfigurator
{
    public IMSHelperBuilder Builder { get; }
    public OutboxOptions Options { get; }

    public MessageOutboxConfigurator(IMSHelperBuilder builder, OutboxOptions options)
    {
        Builder = builder;
        Options = options;
    }
}