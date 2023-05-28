using log4net;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Web.App_Start;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NextItemBuy.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog _log = LogManager.GetLogger("WEB");
        protected void Application_Start()
        {
            _log.Error("project init");
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            var app = sender as HttpApplication;

            var modelStateException = ex as InvalidModelStateException;
            if (modelStateException != null)
            {
                HandleException(app, modelStateException.Message);
                return;
            }

            var customException = ex as CustomException;
            if (customException != null)
            {
                HandleException(app, customException.Message);
                return;
            }

            var notImplementedException = ex as NotImplementedException;
            if (notImplementedException != null)
            {
                HandleApplicationException(app, notImplementedException.Message);
                return;
            }

            var applicationException = ex as ApplicationException;
            if (applicationException != null)
            {
                HandleApplicationException(app, applicationException.Message);
                return;
            }

            var argumentNullException = ex as ArgumentNullException;
            if (argumentNullException != null)
            {
                SetCustomError(app, argumentNullException.Message, 401);
                return;
            }

            var serverError = Server.GetLastError() as HttpException;
            if (serverError == null)
            {
                SetCustomError(app, Server.GetLastError().Message, 401);
            }
            else
            {
                var errorCode = serverError.GetHttpCode();
                if (404 != errorCode)
                {
                    SetCustomError(app, serverError.Message, 401);
                    return;
                }
            }
            Server.ClearError();
            Server.Transfer(HttpContext.Current.Request.Url.Host);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.End();
            }
        }

        private void HandleException(HttpApplication app, string errorMsg)
        {
            var message = errorMsg.Replace("model.", "");
            SetCustomError(app, message, 400);
        }
        private void HandleApplicationException(HttpApplication app, string errorMsg)
        {
            var message = errorMsg.Replace("model.", "");
            message = FormatErrorMessage(message);
            SetCustomError(app, message, 402);
        }

        private string FormatErrorMessage(string message)
        {
            return "{" + string.Format("\"Message\":\"{0}\"", message) + "}";
        }

        private void SetCustomError(HttpApplication app, string message, int errorCode)
        {
            app.Context.ClearError();
            app.Context.Response.TrySkipIisCustomErrors = true;
            app.Context.Response.Write(message);
            app.Context.Response.ContentType = "application/json; charset=utf-8";
            app.Context.Response.StatusCode = errorCode;
        }
    }
}
