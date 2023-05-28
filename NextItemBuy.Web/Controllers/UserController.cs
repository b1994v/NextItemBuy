using Microsoft.Practices.ServiceLocation;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Model;
using System.Security.Principal;
using System.Web.Http;

namespace NextItemBuy.Web.Controllers
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IAuthenticationService _authenticationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            var result = _authenticationService.Login(model);


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