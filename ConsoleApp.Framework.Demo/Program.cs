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


    public class ServiceManager
    {
        [DIInject]
        public TestService service { get; set; }

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
    public class TestService : ServiceSetter
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

    public interface IInterfaceServiceA
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
