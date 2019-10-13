using Autofac;

namespace ResponsibleSystem.Common.Infrastructure
{
    public interface IDependencyResolverFactory
    {
        IContainer BuildContainer();
    }
}
