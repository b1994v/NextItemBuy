using System.Security.Principal;

namespace NextItemBuy.Services.Interfaces
{
    public interface IStatisticsService
    {
        dynamic LoadStatistics(IPrincipal user);
    }
}
