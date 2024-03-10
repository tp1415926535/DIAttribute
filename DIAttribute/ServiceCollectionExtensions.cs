using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIAttribute
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// register all service use DIRegisterAttribute / DIRegisterInterfaceAttribute (CallingAssembly) 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCurrentAssembly(this IServiceCollection services, Func<Type, bool> typeFilter = null)
        {
            return services.RegisterAssembly(Assembly.GetCallingAssembly(), typeFilter);
        }

        /// <summary>
        /// register all service use DIRegisterAttribute / DIRegisterInterfaceAttribute 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssembly(this IServiceCollection services, Assembly assembly, Func<Type, bool> typeFilter = null)
        {
            var types = assembly.GetTypes();
            if (typeFilter != null)
                types = types.Where(typeFilter).ToArray();
            foreach (var type in types)
            {
                //register from class
                foreach (DIRegisterAttribute registerAttribute in type.GetCustomAttributes<DIRegisterAttribute>())
                {
                    Type outSideType = registerAttribute.InterfaceType ?? type;
                    if (!outSideType.IsAssignableFrom(type))
                        throw new ArgumentException($"The concrete type '{type.FullName}' cannot be used to register service type '{outSideType.FullName}'.");

                    var serviceDescriptor = new ServiceDescriptor(outSideType, type, registerAttribute.LifeTime);
                    services.TryAdd(serviceDescriptor);
                }
                //register from interface
                foreach (DIRegisterInterfaceAttribute registerAttribute in type.GetCustomAttributes<DIRegisterInterfaceAttribute>())
                {
                    Type serviceType = registerAttribute.BaseServiceType ?? type;
                    if (!type.IsAssignableFrom(serviceType))
                        throw new ArgumentException($"The concrete type '{type.FullName}' cannot be used to register service type '{serviceType.FullName}'.");

                    var serviceDescriptor = new ServiceDescriptor(type, serviceType, registerAttribute.LifeTime);
                    services.TryAdd(serviceDescriptor);
                }
            }
            return services;
        }
    }
    public static class IServiceProviderExtensions
    {
        public static IServiceProvider ForDIInject(this IServiceProvider serviceProvider, object classInstance = null)
        {
            ServiceSetter.InjectServices(serviceProvider, classInstance);
            return serviceProvider;
        }
    }
}
