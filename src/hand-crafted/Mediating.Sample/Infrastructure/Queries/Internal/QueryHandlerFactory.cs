using System;

namespace Mediating.Sample.Infrastructure.Queries.Internal
{
    internal class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IAbstractFactory _abstractFactory;

        public QueryHandlerFactory(IAbstractFactory abstractFactory)
        {
            if(abstractFactory == null)
                throw new ArgumentNullException(nameof(abstractFactory));

            _abstractFactory = abstractFactory;
        }

        public QueryHandlerWrapper<TResult> GetHandler<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handlerWrapperType = typeof(QueryHandlerWrapper<,>).MakeGenericType(query.GetType(), typeof(TResult));

            var handler = _abstractFactory.GetService(handlerType);

            var result = (QueryHandlerWrapper<TResult>) Activator.CreateInstance(handlerWrapperType, handler);
            return result;
        }
    }
}
