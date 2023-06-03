using System;

namespace NextItemBuy.Domain
{
    public partial class Bank
    {
        public Bank()
        {

        }

        public Bank(Guid userId, int budget, bool isIncome, string reason)
        {
            UserId = userId;
            Budget = budget;
            IsIncome = isIncome;
            Reason = reason;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
