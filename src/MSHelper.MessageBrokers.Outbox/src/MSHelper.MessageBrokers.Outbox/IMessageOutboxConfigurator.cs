namespace MSHelper.MessageBrokers.Outbox;

public interface IMessageOutboxConfigurator
{
    IMSHelperBuilder Builder { get; }
    OutboxOptions Options { get; }
}