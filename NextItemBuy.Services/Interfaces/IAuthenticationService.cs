using NextItemBuy.Services.Model;
using System.Security.Principal;

namespace NextItemBuy.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserModel Login(LoginModel model);
        void Register(UserModel model);
    }
}
