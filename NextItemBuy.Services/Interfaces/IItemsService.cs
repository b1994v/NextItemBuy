using NextItemBuy.Services.Model;
using NextItemBuy.Services.Model.SearchModels;
using NextItemBuy.Services.Model.ViewModel;
using System.Collections.Generic;
using System.Security.Principal;

namespace NextItemBuy.Services.Interfaces
{
    public interface IItemsService
    {
        List<ItemViewModel> LoadItems(ItemsSearchModel searchModel, out int total);
        void SaveOrUpdate(ItemViewModel model, IPrincipal user);
        ItemViewModel LoadItemDetails(int itemId);
        void DeleteItem(int itemId);
        void SetItemToBuyed(int itemId);
        List<ItemCategoryViewModel> LoadCategories(IPrincipal user);
    }
}
