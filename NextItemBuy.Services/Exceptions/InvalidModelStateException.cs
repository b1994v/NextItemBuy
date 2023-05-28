using System;
using System.Web.Mvc;

namespace NextItemBuy.Services.Exceptions
{
    public class InvalidModelStateException: Exception
    {
        public InvalidModelStateException(ModelStateDictionary modelState)
            : base(new { Errors = modelState.Errors() }.ToJson())
        {
        }
    }
}
