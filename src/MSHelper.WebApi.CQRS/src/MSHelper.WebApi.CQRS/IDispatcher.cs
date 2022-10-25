using System.Threading;
using System.Threading.Tasks;
using MSHelper.CQRS.Commands;
using MSHelper.CQRS.Events;
using MSHelper.CQRS.Queries;

namespace MSHelper.WebApi.CQRS;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}