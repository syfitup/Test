using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;
using SYF.Infrastructure.Configuration;

namespace SYF.Infrastructure
{
    public class ModuleLoader
    {
        public ModuleLoader(ContainerBuilder builder)
        {
            ContainerBuilder = builder;
        }

        public ContainerBuilder ContainerBuilder { get; }

        public void Load(IEnumerable<ModuleOptions> moduleDefinitions)
        {
            foreach (var moduleDefinition in moduleDefinitions)
            {
                var moduleAssembly = Assembly.Load(moduleDefinition.AssemblyName);
                var moduleType = moduleAssembly.GetType(moduleDefinition.TypeName);
                var module = Activator.CreateInstance(moduleType) as IModule;

                ContainerBuilder.RegisterModule(module);
            }
        }
    }
}

