using NextItemBuy.Services.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;

namespace NextItemBuy.Web.Helpers
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {

            var modelStateException = context.Exception as InvalidModelStateException;
            if (modelStateException != null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(new JavaScriptSerializer().Serialize(modelStateException.Message))
                };
            }

            var customException = context.Exception as CustomException;
            if (customException != null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(new JavaScriptSerializer().Serialize(customException.Errors))
                };
            }
        }
    }
}