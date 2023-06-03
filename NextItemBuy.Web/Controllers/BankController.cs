using Microsoft.Practices.ServiceLocation;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Model.SearchModels;
using NextItemBuy.Services.Model.ViewModel;
using System.Web.Http;

namespace NextItemBuy.Web.Controllers
{
    [RoutePrefix("api/bank")]
    [Authorize]
    public class BankController : ApiController
    {
        private readonly IBankService _bankService = ServiceLocator.Current.GetInstance<IBankService>();

        [HttpPost]
        [Route("load-funds")]
        public IHttpActionResult LoadFunds([FromBody] BankSearchModel searchModel)
        {
            var items = _bankService.LoadFunds(searchModel, out int total);
            return Ok(new { items, total });
        }

        [HttpPost]
        [Route("add-funds")]
        public IHttpActionResult AddFunds([FromBody] BankViewModel model)
        {
            _bankService.AddFunds(model, User);
            return Ok(true);
        }

        [HttpPost]
        [Route("delete-funds")]
        public IHttpActionResult DeleteFunds([FromBody] int fundsId)
        {
            _bankService.DeleteFunds(fundsId);
            return Ok(true);
        }

        [HttpGet]
        [Route("load-items-by-priority")]
        public IHttpActionResult LoadItemsByPriority()
        {
            var result = _bankService.LoadItemsByPriority(User);
            return Ok(result);
        }

        [HttpGet]
        [Route("load-user-total-funds")]
        public IHttpActionResult LoadUserTotalFunds()
        {
            var result = _bankService.LoadUserTotalFunds(User);
            return Ok(result);
        }
    }
}