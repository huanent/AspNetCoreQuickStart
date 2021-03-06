﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MyCompany.MyProject.Web.Internal
{
    internal static class AddInjectByAttributeExtensions
    {
        public static IServiceCollection AddInject(this IServiceCollection services)
        {
            var assemblys = GetAppAssemblies();
            var types = assemblys.SelectMany(s => s.GetTypes()).Distinct();

            var injectScopedType = typeof(InjectScopedAttribute);
            var injectSingletonType = typeof(InjectSingletonAttribute);
            var injectTransientType = typeof(InjectTransientAttribute);

            AddServices(services, types, injectScopedType, ServiceLifetime.Scoped);
            AddServices(services, types, injectSingletonType, ServiceLifetime.Singleton);
            AddServices(services, types, injectTransientType, ServiceLifetime.Transient);

            return services;
        }

        private static void AddServices(
            IServiceCollection services,
            IEnumerable<Type> types,
            Type injectType,
            ServiceLifetime serviceLifetime)
        {
            var registedTypes = types.Where(w => w.CustomAttributes.Any(a => a.AttributeType == injectType));

            foreach (var item in registedTypes)
            {
                dynamic att = item.GetCustomAttribute(injectType, false);
                var serviceDescriptor = default(ServiceDescriptor);

                if (att.Type != null)
                {
                    var type = types.First(f => f.GUID == att.Type.GUID);
                    serviceDescriptor = new ServiceDescriptor(type, item, serviceLifetime);
                }
                else
                {
                    serviceDescriptor = new ServiceDescriptor(item, serviceLifetime);
                }

                services.Add(serviceDescriptor);
            }
        }

        private static Assembly[] GetAppAssemblies()
        {
            var assemblies = new List<Assembly>();
            IEnumerable<Assembly> recursive(Assembly rootAssembly)
            {
                foreach (var AssemblyName in rootAssembly.GetReferencedAssemblies())
                {
                    if (AssemblyName.FullName.Contains(Constants.AppName))
                    {
                        var assembly = Assembly.Load(AssemblyName);
                        yield return assembly;
                        foreach (var item in recursive(assembly))
                        {
                            yield return item;
                        }
                    }
                }
            }
            var currentAssembly = Assembly.GetExecutingAssembly();
            assemblies.Add(currentAssembly);
            assemblies.AddRange(recursive(currentAssembly));
            return assemblies.Distinct().ToArray();
        }
    }
}
