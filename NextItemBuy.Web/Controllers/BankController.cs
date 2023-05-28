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
        [AllowAnonymous]
        public IHttpActionResult LoadFunds([FromBody] BankSearchModel searchModel)
        {
            var items = _bankService.LoadFunds(searchModel, out int total);
            return Ok(new { items, total });
        }

        [HttpPost]
        [Route("add-funds")]
        [AllowAnonymous]
        public IHttpActionResult AddFunds([FromBody] BankViewModel model)
        {
            _bankService.AddFunds(model);
            return Ok(true);
        }

        [HttpPost]
        [Route("delete-funds")]
        [AllowAnonymous]
        public IHttpActionResult DeleteFunds([FromBody] int fundsId)
        {
            _bankService.DeleteFunds(fundsId);
            return Ok(true);
        }
    }
}