using System.Threading;
using System.Threading.Tasks;

namespace MSHelper.CQRS.Events;

public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}