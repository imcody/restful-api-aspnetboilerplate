using Autofac;
using ResponsibleSystem.Common.Infrastructure;
using ResponsibleSystem.Common.Logs;

namespace ResponsibleSystem.Common.WebJobs
{
    public class WebJobBootstrapper<TJob> where TJob : IJob
    {
        private readonly IDependencyResolverFactory _resolverFactory;

        public string JobName => typeof(TJob).Name;

        public WebJobBootstrapper(IDependencyResolverFactory resolverFactory)
        {
            _resolverFactory = resolverFactory;
        }

        public virtual void Run()
        {
            var container = _resolverFactory.BuildContainer();
            using (var lifetimescope = container.BeginLifetimeScope())
            {
                var logger = lifetimescope.Resolve<ILogger>();
                logger.Info($"{JobName} started");

                var service = lifetimescope.Resolve<TJob>();
                service.Start();

                logger.Info($"{JobName} ended - shutting down.");
            }
        }
    }
}
