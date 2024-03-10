using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIAttribute
{
    /// <summary>
    /// Should be explicitly inherited
    /// </summary>
    public class ServiceSetter
    {
        public static IServiceProvider _serviceProvider { get; set; }

        /// <summary>
        /// Inherited class initialization should automatically go this method
        /// </summary>
        public ServiceSetter()
        {
            if (_serviceProvider == null)
                throw new ArgumentException($"Make sure that IServiceProvider calls '.ForDIInject()' ,and is not in the class that created the IServiceProvider!");

            SetService(this);
        }

        /// <summary>
        /// save serviceProvider and set property which DIInjectAttribute in the class that created the IServiceProvider
        /// </summary>
        /// <param name="classInstance"></param>
        public static void InjectServices(IServiceProvider serviceProvider, object classInstance)
        {
            _serviceProvider = serviceProvider;
            if (classInstance == null) return;
            SetService(classInstance);
        }

        /// <summary>
        ///set property which DIInjectAttribute before init
        /// </summary>
        /// <param name="classInstance"></param>
        private static void SetService(object classInstance)
        {
            //set property which DIInjectAttribute before init
            foreach (PropertyInfo propertyInfo in classInstance.GetType().GetProperties())
            {
                object[] attributes = propertyInfo.GetCustomAttributes(typeof(DIInjectAttribute), false);
                foreach (DIInjectAttribute attribute in attributes)
                {
                    object service = null;
                    if (attribute.Required)
                        service = _serviceProvider.GetRequiredService(propertyInfo.PropertyType);
                    else
                        service = _serviceProvider.GetService(propertyInfo.PropertyType);
                    propertyInfo.SetValue(classInstance, service);
                }
            }
        }
    }
}
