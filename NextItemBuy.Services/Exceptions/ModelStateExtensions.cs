using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NextItemBuy.Services.Exceptions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return (IEnumerable)modelState.Keys.Where<string>
                ((Func<string, bool>)(key => modelState[key].Errors.Any<ModelError>()))
                .SelectMany((Func<string, IEnumerable<ModelError>>)
                (key => (IEnumerable<ModelError>)modelState[key].Errors),
                (key, error) => new
                {
                    Key = key,
                    Message = error.ErrorMessage
                }).ToList();

            return (IEnumerable)null;
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
