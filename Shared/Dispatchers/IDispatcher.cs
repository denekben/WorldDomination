using Shared.Commands;
using Shared.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}
