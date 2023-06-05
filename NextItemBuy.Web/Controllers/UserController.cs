using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Model;
using NextItemBuy.Services.Utils;
using NextItemBuy.Web.Helpers;
using System.Collections.Generic;
using System.IO;
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
        public IHttpActionResult Register()
        {
            var request = HttpContext.Current.Request;

            var model = JsonConvert.DeserializeObject<UserModel>(request.Form["model"]);

            var file = request.Files["file"];
            model.FileName = file.FileName;

            _authenticationService.Register(model, file);
            return Ok(false);
        }
    }
}