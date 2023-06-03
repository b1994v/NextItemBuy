using Microsoft.Practices.ServiceLocation;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Model;
using NextItemBuy.Web.Helpers;
using System.Web;
using System.Web.Http;

namespace NextItemBuy.Web.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IAuthenticationService _authenticationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            var result = _authenticationService.Login(model);

            HttpContext.Current.GenerateToken(result);
            
            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IHttpActionResult Register([FromBody] UserModel model)
        {
            _authenticationService.Register(model);
            return Ok(true);
        }
    }
}