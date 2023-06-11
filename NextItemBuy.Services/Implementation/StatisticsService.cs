using NextItemBuy.Domain;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;

namespace NextItemBuy.Services.Implementation
{
    public class StatisticsService: IStatisticsService
    {
        public object LoadStatistics(IPrincipal user)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var userModel = ctx.Users.FirstOrDefault(x => x.Username == user.Identity.Name);
                if (userModel == null)
                {
                    throw new CustomException("User not found!");
                }

                var incomeStatistics = new List<StatisticsViewModel>();
                var incomeFunds = ctx.Banks.Where(x => x.UserId == userModel.Id && x.IsIncome).ToList().OrderBy(x => x.CreatedOn).ToList();

                var outcomeStatistics = new List<StatisticsViewModel>();
                var outcomeFunds = ctx.Banks.Where(x => x.UserId == userModel.Id &&  !x.IsIncome).ToList().OrderBy(x => x.CreatedOn).ToList();

                for (var i = 1; i <= DateTime.Now.Month; i++)
                {
                    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);

                    var totalIncomeByMonth = incomeFunds.Where(x => x.CreatedOn.Value.Month == i).Sum(x => x.Budget);
                    incomeStatistics.Add(new StatisticsViewModel
                    {
                        Name = month,
                        Value = totalIncomeByMonth
                    });

                    var totalOutcomeByMonth = outcomeFunds.Where(x => x.CreatedOn.Value.Month == i).Sum(x => x.Budget);
                    outcomeStatistics.Add(new StatisticsViewModel
                    {
                        Name = month,
                        Value = totalOutcomeByMonth
                    });
                }

                return new { incomeStatistics, outcomeStatistics };

            }

        }
    }
}
