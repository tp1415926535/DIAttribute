# DIAttribute
* [DIRegister]
* [DIInject]

[![release](https://img.shields.io/github/v/release/tp1415926535/DIAttribute?color=green&logo=github)](https://github.com/tp1415926535/DIAttribute/releases) 
[![nuget](https://img.shields.io/nuget/v/DIAttribute?color=lightblue&logo=nuget)](https://www.nuget.org/packages/DiAttribute)     
![language](https://img.shields.io/github/languages/top/tp1415926535/DIAttribute)
     
[![English Description](https://img.shields.io/static/v1?label=English&message=Description&color=yellow)](https://github.com/tp1415926535/DIAttribute?tab=readme-ov-file#english-description) 
[![中文介绍](https://img.shields.io/static/v1?label=%E4%B8%AD%E6%96%87&message=%E8%AF%B4%E6%98%8E&color=red)](https://github.com/tp1415926535/DIAttribute?tab=readme-ov-file#%E4%B8%AD%E6%96%87%E4%BB%8B%E7%BB%8D)

## English Description
### [DIRegister]
* Raw way

registering for a service list:
```C#
var services = new ServiceCollection();
services.AddSingleton<TestService1>();
services.AddSingleton<ITestService2>();
//...
var provider = services.BuildServiceProvider();
```
services:
```C#
class TestService1
{
  //...
}
class TestService2
{
  //...
}
interface ITestService2 {}
```
* Current

registering for a service list:
```C#
var services = new ServiceCollection();
services.RegisterCurrentAssembly(); // just add this, or services.RegisterAssembly(assmbly);
var provider = services.BuildServiceProvider();
```
services:
```C#
[DIRegister] //add attribute if need register
class TestService1
{
  //...
}
[DIRegister(typeof(ITestService2))] // if use interface
class TestService2
{
  //...
}
interface ITestService2 {}
```
### [DIInject]
* Raw way
```C#
class TestService1
{
    ITestService2 _service2 { get; set; }

    public TestService1(ITestService2 service2 /*,TestService3 service3...*/)
    {
        _service2 = service2;
        //_service3 = service3;

        _service2.Method();
    }
}
```
* Current

registering for a service list:
```C#
var services = new ServiceCollection();
//...
var provider = services.BuildServiceProvider().ForDIInject(this); //add method
```
class property:
```C#
class TestService1 : ServiceSetter // Inheriting this class automatically injects property values
{
    [DIInject] //add attribute if need inject
    ITestService2 _service2 { get; set; }

    public TestService1()
    {
        _service2.Method(); // can get service in constructor
    }
}
```
If you don't want to inherit from this base class, you can also call the property injection method manually:
```C#
class TestService1
{
    [DIInject] //add attribute if need inject
    ITestService2 _service2 { get; set; }

    public TestService1()
    {
        ServiceSetter.InjectServices(this);// manually call without inherit class
        _service2.Method(); // get service after InjectServices
    }
}
```

-------------
## 中文介绍
### [DIRegister]
* 原生写法

初始化注册服务到集合：
```C#
var services = new ServiceCollection();
services.AddSingleton<TestService1>();
services.AddSingleton<ITestService2>();
//...
var provider = services.BuildServiceProvider();
```
service服务示例：
```C#
class TestService1
{
  //...
}
class TestService2
{
  //...
}
interface ITestService2 {}
```
* 现在写法

初始化注册服务到集合：
```C#
var services = new ServiceCollection();
services.RegisterCurrentAssembly(); // 只要添加这行, 或者指定 services.RegisterAssembly(assmbly);
var provider = services.BuildServiceProvider();
```
service服务示例：
```C#
[DIRegister] // 如果要注册的服务就添加特性
class TestService1
{
  //...
}
[DIRegister(typeof(ITestService2))] // 如果有接口则指定
class TestService2
{
  //...
}
interface ITestService2 {}
```
### [DIInject]
* 原生写法
```C#
class TestService1
{
    ITestService2 _service2 { get; set; }

    public TestService1(ITestService2 service2 /*,TestService3 service3...*/)
    {
        _service2 = service2;
        //_service3 = service3;

        _service2.Method();
    }
}
```
* 现在写法

初始化注册服务到集合：
```C#
var services = new ServiceCollection();
//...
var provider = services.BuildServiceProvider().ForDIInject(this); //补充调用方法用于后续注入获取
```
类写法:
```C#
class TestService1 : ServiceSetter //继承这个类会自动给有特性的属性赋值
{
    [DIInject] //如果需要注入添加这个特性
    ITestService2 _service2 { get; set; }

    public TestService1()
    {
        _service2.Method();//服务可以在构造函数中获取到
    }
}
```
如果不想要继承这个基类，也可以手动调用属性注入方法：
```C#
class TestService1
{
    [DIInject] //如果需要注入添加这个特性
    ITestService2 _service2 { get; set; }

    public TestService1()
    {
        ServiceSetter.InjectServices(this);// 手动调用注入属性服务
        _service2.Method(); //服务在注入后可以获取到
    }
}
```
