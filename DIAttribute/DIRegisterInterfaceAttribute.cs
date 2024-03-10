using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAttribute
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class DIRegisterInterfaceAttribute : Attribute
    {
        /// <summary>
        /// Base Service
        /// </summary>
        public Type BaseServiceType { get; set; }

        /// <summary>
        /// DI register LifeTime
        /// </summary>
        public ServiceLifetime LifeTime { get; } = ServiceLifetime.Singleton;


        public DIRegisterInterfaceAttribute(Type baseService)
        {
            BaseServiceType = baseService;
        }
    }
}
