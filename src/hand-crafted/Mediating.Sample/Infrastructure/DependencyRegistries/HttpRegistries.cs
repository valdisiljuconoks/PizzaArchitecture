using Microsoft.AspNetCore.Http;
using StructureMap;

namespace Mediating.Sample.Infrastructure.DependencyRegistries
{
    public class HttpRegistries : Registry
    {
        public HttpRegistries()
        {
            For<IHttpContextAccessor>().Use<HttpContextAccessor>().Singleton();
        }
    }
}
