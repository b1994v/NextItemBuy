
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NextItemBuy.Web.Helpers
{
    public class AuthentificationHelper: AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var ctx = HttpContext.Current;

            return ctx.ValidateToken();
        }
    }
}