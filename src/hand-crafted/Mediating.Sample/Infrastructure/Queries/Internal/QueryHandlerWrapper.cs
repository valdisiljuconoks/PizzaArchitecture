namespace Mediating.Sample.Infrastructure.Queries.Internal
{
    internal abstract class QueryHandlerWrapper<TResult>
    {
        public abstract TResult Handle(IQuery<TResult> query);
    }

    internal class QueryHandlerWrapper<TQuery, TResult> : QueryHandlerWrapper<TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _inner;

        public QueryHandlerWrapper(IQueryHandler<TQuery, TResult> inner)
        {
            _inner = inner;
        }

        public override TResult Handle(IQuery<TResult> query)
        {
            return _inner.Handle((TQuery) query);
        }
    }
}
