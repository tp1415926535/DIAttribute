using DIAttribute;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Framework.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.RegisterCurrentAssembly();
            var provider = services.BuildServiceProvider().ForDIInject();

            provider.GetService<IInterfaceServiceA>().Show();

            Console.WriteLine(provider.GetService<TestService>().otherService == null);
            Console.Read();
        }
    }

    #region without interface
    [DIRegister]
    class TestService: ServiceSetter
    {
        [DIInject]
        public IInterfaceServiceA otherService { get; set; }

        public TestService()
        {
            Console.WriteLine(otherService == null);
        }
            
                                                                                
        public void Show()
        {
            Console.WriteLine("hello");
        }
    }
    #endregion


    #region register from class
    [DIRegister(typeof(IInterfaceServiceA))]
    class InterfaceServiceA : IInterfaceServiceA
    {
        public void Show()
        {
            Console.WriteLine("InterfaceA Success");
        }
    }

    interface IInterfaceServiceA
    {
        void Show();
    }
    #endregion


    #region register from interface
    class InterfaceServiceB : IInterfaceServiceB
    {
        public void Show()
        {
            Console.WriteLine("InterfaceB Success");
        }
    }

    [DIRegisterInterface(typeof(InterfaceServiceB))]
    interface IInterfaceServiceB
    {
        void Show();
    }
    #endregion


}
