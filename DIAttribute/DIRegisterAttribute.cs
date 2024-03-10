using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIAttribute
{
    /// <summary>
    /// To Register
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DIRegisterAttribute : Attribute
    {
        /// <summary>
        /// if has Interface
        /// </summary>
        public Type InterfaceType { get; set; }

        /// <summary>
        /// DI register LifeTime
        /// </summary>
        public ServiceLifetime LifeTime { get; } = ServiceLifetime.Singleton;


        /// <summary>
        /// if interface 
        /// </summary>
        public DIRegisterAttribute(Type interfaceType = null)
        {
            InterfaceType = interfaceType;
        }
    }
}
