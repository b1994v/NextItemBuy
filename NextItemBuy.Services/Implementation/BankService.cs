using NextItemBuy.Domain;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Mapper;
using NextItemBuy.Services.Model;
using NextItemBuy.Services.Model.SearchModels;
using NextItemBuy.Services.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace NextItemBuy.Services.Implementation
{
    public class BankService: IBankService
    {
        public List<BankViewModel> LoadFunds(BankSearchModel searchModel, out int total, IPrincipal user)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var userModel = ctx.Users.FirstOrDefault(x => x.Username == user.Identity.Name);
                if (userModel == null)
                {
                    throw new CustomException("User not found!");
                }

                var query = ctx.Banks.Where(x => x.UserId == userModel.Id).ToList();

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

        public void AddFunds(BankViewModel model, IPrincipal user)
        {
            var validator = new BankViewModelValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                throw new CustomException(result.Errors.Select(x => new ValidationRecord(x.PropertyName, x.ErrorMessage)).ToList());
            }

            using(var ctx = new NextItemBuyEntities())
            {
                var userModel = ctx.Users.FirstOrDefault(x => x.Username == user.Identity.Name);
                if(userModel == null)
                {
                    throw new CustomException("User not found!");
                }

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

                item = new Bank(userModel.Id, model.Budget, model.IsIncome, model.Reason);

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

        public dynamic LoadItemsByPriority(IPrincipal user)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var userModel = ctx.Users.FirstOrDefault(x => x.Username == user.Identity.Name);
                if(userModel == null)
                {
                    throw new CustomException("User not found!");
                }

                var funds = LoadUserTotalFunds(user);

                var query = ctx.Items.Where(x => x.UserId == userModel.Id && !x.IsBuyed).OrderByDescending(x => x.Deadline);

                var items = new List<ItemViewModel>();
                decimal totalItemsSum = 0;

                foreach (var item in query)
                {
                    if(totalItemsSum + item.Price <= funds)
                    {
                        totalItemsSum += item.Price;
                        items.Add(item.ToViewModel());
                    }
                }
                return new { funds, items };
            }
        }

        public decimal LoadUserTotalFunds(IPrincipal user)
        {
            using (var ctx = new NextItemBuyEntities())
            {
                var userModel = ctx.Users.FirstOrDefault(x => x.Username == user.Identity.Name);
                if (userModel == null)
                {
                    throw new CustomException("User not found!");
                }

                var incomeFunds = ctx.Banks.Where(x => x.UserId == userModel.Id && x.IsIncome).ToList();
                var outcomeFunds = ctx.Banks.Where(x => x.UserId == userModel.Id && !x.IsIncome).ToList();

                var funds = (incomeFunds.Count() != 0 ? incomeFunds.Sum(x => x.Budget) : 0) - (outcomeFunds.Count() != 0 ? outcomeFunds.Sum(x => x.Budget) : 0);

                return funds;
            }
        }
    }
}
