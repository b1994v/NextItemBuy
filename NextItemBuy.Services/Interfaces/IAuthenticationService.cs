using NextItemBuy.Services.Model;
using System.Security.Principal;
using System.Web;

namespace NextItemBuy.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserModel Login(LoginModel model);
        void Register(UserModel model, HttpPostedFile file);
    }
}
