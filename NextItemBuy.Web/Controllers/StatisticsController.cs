using Microsoft.Practices.ServiceLocation;
using NextItemBuy.Services.Interfaces;
using System.Web.Http;

namespace NextItemBuy.Web.Controllers
{
    [RoutePrefix("api/statistics")]
    [Authorize]
    public class StatisticsController: ApiController
    {
        private readonly IStatisticsService _statisticsService = ServiceLocator.Current.GetInstance<IStatisticsService>();

        //[HttpPost]
        //[Route("load-items")]
        //[AllowAnonymous]
        //public IHttpActionResult LoadItems([FromBody] ItemsSearchModel searchModel)
        //{
        //    var items = _itemsService.LoadItems(searchModel, out int total);
        //    return Ok(new { items, total });
        //}
    }
}