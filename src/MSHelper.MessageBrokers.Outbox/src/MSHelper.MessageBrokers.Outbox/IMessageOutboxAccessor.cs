using System.Collections.Generic;
using System.Threading.Tasks;
using MSHelper.MessageBrokers.Outbox.Messages;

namespace MSHelper.MessageBrokers.Outbox;

public interface IMessageOutboxAccessor
{
    Task<IReadOnlyList<OutboxMessage>> GetUnsentAsync();
    Task ProcessAsync(OutboxMessage message);
    Task ProcessAsync(IEnumerable<OutboxMessage> outboxMessages);
}