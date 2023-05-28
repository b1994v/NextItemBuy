using System;

namespace NextItemBuy.Domain
{
    public partial class Item
    {
        public Item()
        {

        }
        public Item(int userId, string name, string description, decimal price, DateTime deadline, DateTime notificationDate)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Price = price;
            NotificationDate = notificationDate;
            Deadline = deadline;
            IsBuyed = false;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
