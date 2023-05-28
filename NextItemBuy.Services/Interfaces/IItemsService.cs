using NextItemBuy.Services.Model;
using NextItemBuy.Services.Model.SearchModels;
using System.Collections.Generic;

namespace NextItemBuy.Services.Interfaces
{
    public interface IItemsService
    {
        List<ItemViewModel> LoadItems(ItemsSearchModel searchModel, out int total);
        void SaveOrUpdate(ItemViewModel model);
        ItemViewModel LoadItemDetails(int itemId);
        void DeleteItem(int itemId);
        void SetItemToBuyed(int itemId);
    }
}
