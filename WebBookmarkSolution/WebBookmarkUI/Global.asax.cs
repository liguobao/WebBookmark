using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebBookmarkService;
using WebBookmarkUI.Controllers;

namespace WebBookmarkUI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

           System.Net.ServicePointManager.ServerCertificateValidationCallback +=
           delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                   System.Security.Cryptography.X509Certificates.X509Chain chain,
                                   System.Net.Security.SslPolicyErrors sslPolicyErrors)
           {
               return true; // **** Always accept
           };

        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var ex = Server.GetLastError();
            
        //    LogHelper.WriteException("Application_Error", ex, new { Message="应用程序引发异常。",UserIP = this.Request.UserHostAddress});

        //    var httpStatusCode = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500; //这里仅仅区分两种错误
        //    var httpContext = ((MvcApplication)sender).Context;
        //    httpContext.ClearError();
        //    httpContext.Response.Clear();
        //    httpContext.Response.StatusCode = httpStatusCode;
        //    var routeData = new RouteData();
        //    routeData.Values["controller"] = "Error";
        //    routeData.Values["action"] = "Index";  
        //    var controller = new ErrorController();
        //    controller.ViewData.Model = null; //通过代码路由到指定的路径
        //    ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        //}


    }
}