using NextItemBuy.Services.Model.SearchModels;
using NextItemBuy.Services.Model.ViewModel;
using System.Collections.Generic;
using System.Security.Principal;

namespace NextItemBuy.Services.Interfaces
{
    public interface IBankService
    {
        List<BankViewModel> LoadFunds(BankSearchModel searchModel, out int total);
        void AddFunds(BankViewModel model, IPrincipal user);
        void DeleteFunds(int fundsId);
    }
}
