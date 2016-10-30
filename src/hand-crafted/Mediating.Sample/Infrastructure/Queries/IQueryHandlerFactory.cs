using Mediating.Sample.Infrastructure.Queries.Internal;

namespace Mediating.Sample.Infrastructure.Queries
{
    internal interface IQueryHandlerFactory
    {
        QueryHandlerWrapper<TResult> GetHandler<TResult>(IQuery<TResult> query);
    }
}
