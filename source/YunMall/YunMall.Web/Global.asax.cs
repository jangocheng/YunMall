using DF.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YunMall.Entity;
using YunMall.Web.Core;

namespace YunMall.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            /*AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);*/


            //注册区域
            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(System.Threading.Thread.GetDomain().BaseDirectory + "log4net.config"));
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();

            //注册错误过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Constants.Debug = true;
        }
    }
}
