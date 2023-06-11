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

        [HttpGet]
        [Route("load-statistics")]
        public IHttpActionResult LoadStatistics()
        {
            var items = _statisticsService.LoadStatistics(User);
            return Ok(items);
        }
    }
}