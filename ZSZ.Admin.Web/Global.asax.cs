using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZSZ.Admin.Web.App_Start;
using ZSZ.Common;
using ZSZ.IService;

namespace ZSZ.Admin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //程序启动然后启动log4net日志框
            log4net.Config.XmlConfigurator.Configure();
            //增加自定义异常处理
            GlobalFilters.Filters.Add(new ZSZExceptionFilter());
            //全角转半角字符串
            ModelBinders.Binders.Add(typeof(string), new TrimAndDBCModelBinder());
            //全角转半角整数数字
            ModelBinders.Binders.Add(typeof(int), new TrimAndDBCModelBinder());
            //全角转半角长整数数字
            ModelBinders.Binders.Add(typeof(long), new TrimAndDBCModelBinder());
            //全角转半角小数数字
            ModelBinders.Binders.Add(typeof(double), new TrimAndDBCModelBinder());
            //ioc配置(？)
            //ioc配置(？)
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前程序集中的 Controller 都注册
            //获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("ZSZ.Service") };
            //当请求这个程序集下的接口里面的方法时候。就会返回对应的Services类里面的实现
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => !type.IsAbstract
                    && typeof(IServiceSupport).IsAssignableFrom(type))
                    .AsImplementedInterfaces().PropertiesAutowired();
            //Assign：赋值
            //type1.IsAssignableFrom(type2)   type2是否实现了type1接口/type2是否继承自type1
            //typeof(IServiceSupport).IsAssignableFrom(type)只注册实现了IServiceSupport的类
            //避免其他无关的类注册到AutoFac中

            var container = builder.Build();
            //注册系统级别的DependencyResolver，这样当MVC框架创建Controller等对象的时候都是管Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//!!!

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
    
}
