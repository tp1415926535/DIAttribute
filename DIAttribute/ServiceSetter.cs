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
    /// set service property before init
    /// </summary>
    public class ServiceSetter
    {
        IServiceProvider _serviceProvider { get; set; }
        public ServiceSetter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Set();
        }

        public void Set()
        {
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                object[] attributes = propertyInfo.GetCustomAttributes(typeof(DIInjectAttribute), false);
                foreach (DIInjectAttribute attribute in attributes)
                {
                    object service = null;
                    if (attribute.Required)
                        service = _serviceProvider.GetRequiredService(propertyInfo.PropertyType);
                    else
                        service = _serviceProvider.GetService(propertyInfo.PropertyType);
                    propertyInfo.SetValue(this, service);
                }
            }
        }
    }
}
