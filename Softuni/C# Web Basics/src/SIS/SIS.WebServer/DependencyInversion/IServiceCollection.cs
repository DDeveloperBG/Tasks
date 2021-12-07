using System;

namespace SIS.WebServer.DependencyInversion
{
    public interface IServiceCollection
    {
        void Add<TSource, TDestination>();

        object CreateInstance(Type type);
    }
}
