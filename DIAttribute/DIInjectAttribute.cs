using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DIAttribute
{
    /// <summary>
    ///  should DI in init
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DIInjectAttribute: Attribute
    {
        /// <summary>
        /// required or not
        /// </summary>
        public bool Required { get; set; }

        public DIInjectAttribute(bool required = false)
        {
            Required = required;
        }
    }
}
