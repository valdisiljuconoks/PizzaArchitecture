using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Mediating.Sample.Infrastructure
{
    public interface IAbstractFactory
    {
        object GetService(Type serviceType);

        T GetService<T>();

        IEnumerable<object> GetServices(Type serviceType);

        IEnumerable<T> GetServices<T>();
    }

    internal class AbstractFactory : IAbstractFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AbstractFactory(IHttpContextAccessor contextAccessor)
        {
            if(contextAccessor == null)
                throw new ArgumentNullException(nameof(contextAccessor));

            _contextAccessor = contextAccessor;
        }

        public object GetService(Type serviceType)
        {
            return _contextAccessor.HttpContext.RequestServices.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return _contextAccessor.HttpContext.RequestServices.GetService<T>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _contextAccessor.HttpContext.RequestServices.GetServices(serviceType);
        }

        public IEnumerable<T> GetServices<T>()
        {
            return _contextAccessor.HttpContext.RequestServices.GetServices<T>();
        }
    }
}
