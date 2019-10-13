using Autofac;
using Autofac.Core;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutofacNamespace = Autofac;

namespace ResponsibleSystem.Common.Infrastructure
{
    /// <summary>
    /// Injects a logger specifically for the type of object that depends on it.
    /// </summary>
    public class LogInjectionModule : AutofacNamespace.Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Preparing += (new EventHandler<PreparingEventArgs>(LogInjectionModule.OnComponentPreparing));
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            Type t = e.Component.Activator.LimitType;
            e.Parameters = (e.Parameters.Concat<Parameter>((IEnumerable<Parameter>)
                new ResolvedParameter[1]
                {
                    new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>) (
                    (p, i) => p.ParameterType == typeof (ILog)), 
                    ((p, i) => (object) LogManager.GetLogger(t)))
                }));
        }
    }
}
