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
            ServiceManager serviceManager = new ServiceManager();
            serviceManager.CreateService();
        }
    }


    class ServiceManager
    {
        [DIInject]
        TestService service { get; set; }

        public void CreateService()
        {
            var services = new ServiceCollection();
            services.RegisterCurrentAssembly();
            var provider = services.BuildServiceProvider().ForDIInject(this);

            service.Show();
            Console.Read();
        }
    }

    #region without interface
    [DIRegister]
    class TestService : ServiceSetter //Inheriting this class automatically injects property values
    {
        [DIInject]
        IInterfaceServiceA otherService { get; set; }

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
        [DIInject]
        IInterfaceServiceB otherService { get; set; }

        public InterfaceServiceA()
        {
            ServiceSetter.InjectServices(this);//Injections of manually invoked properties
            Console.WriteLine(otherService == null);
        }

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
