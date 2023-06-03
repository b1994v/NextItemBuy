using Microsoft.Practices.ServiceLocation;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Model;
using NextItemBuy.Services.Model.SearchModels;
using System.Web.Http;

namespace NextItemBuy.Web.Controllers
{
    [RoutePrefix("api/items")]
    [Authorize]
    public class ItemsController: ApiController
    {
        private readonly IItemsService _itemsService = ServiceLocator.Current.GetInstance<IItemsService>();

        [HttpPost]
        [Route("load-items")]
        public IHttpActionResult LoadItems([FromBody] ItemsSearchModel searchModel)
        {
            var items = _itemsService.LoadItems(searchModel, out int total);
            return Ok(new { items, total });
        }

        [HttpPost]
        [Route("save-item")]
        public IHttpActionResult SaveOrUpdate([FromBody] ItemViewModel model)
        {
            _itemsService.SaveOrUpdate(model, User);
            return Ok(true);
        }

        [HttpPost]
        [Route("get-item-details")]
        public IHttpActionResult GetItemDetails([FromBody] int itemId)
        {
            var result = _itemsService.LoadItemDetails(itemId);
            return Ok(result);
        }

        [HttpPost]
        [Route("delete-item")]
        public IHttpActionResult DeleteItem([FromBody] int itemId)
        {
            _itemsService.DeleteItem(itemId);
            return Ok(true);
        }

        [HttpPost]
        [Route("buyed-item")]
        public IHttpActionResult SetItemToBuyed([FromBody] int itemId)
        {
            _itemsService.SetItemToBuyed(itemId);
            return Ok(true);
        }
    }
}