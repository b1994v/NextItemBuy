using NextItemBuy.Domain;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Mapper;
using NextItemBuy.Services.Model.SearchModels;
using NextItemBuy.Services.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NextItemBuy.Services.Implementation
{
    public class BankService: IBankService
    {
        public List<BankViewModel> LoadFunds(BankSearchModel searchModel, out int total)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var query = ctx.Banks.ToList();

                if (searchModel.DateFor != null)
                {
                    query = query.Where(x => x.ModifiedOn.Date.Equals(searchModel.DateFor.Value.Date)).ToList();
                }

                total = query.Count();

                return query.OrderByDescending(x => x.ModifiedOn)
                            .Skip((searchModel.Page - 1) * searchModel.PageSize)
                            .Take(searchModel.PageSize)
                            .ToList()
                            .Select(x => x.ToViewModel())
                            .ToList();
            }
        }

        public void AddFunds(BankViewModel model)
        {
            var validator = new BankViewModelValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                throw new CustomException(result.Errors.Select(x => new ValidationRecord(x.PropertyName, x.ErrorMessage)).ToList());
            }

            using(var ctx = new NextItemBuyEntities())
            {
                var item = ctx.Banks.FirstOrDefault(x => x.Id == model.Id);
                if(item != null)
                {
                    item.Reason = model.Reason;
                    item.Budget = model.Budget;
                    item.IsIncome = model.IsIncome;
                    item.ModifiedOn = model.ModifiedOn;

                    ctx.SaveChanges();
                    return;
                }

                item = new Bank(1, model.Budget, model.IsIncome, model.Reason);

                ctx.Banks.Add(item);
                ctx.SaveChanges();
            }
        }

        public void DeleteFunds(int fundsId)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var item = ctx.Banks.FirstOrDefault(x => x.Id == fundsId);
                if(item == null)
                {
                    throw new ApplicationException("Funds item not found");
                }

                ctx.Banks.Remove(item);
                ctx.SaveChanges();
            }
        }
    }
}
