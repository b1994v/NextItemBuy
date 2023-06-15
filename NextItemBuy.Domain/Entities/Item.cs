using System;

namespace NextItemBuy.Domain
{
    public partial class Item
    {
        public Item()
        {

        }
        public Item(Guid userId, string name, string description, decimal price, int itemCategoryId, DateTime deadline, DateTime notificationDate)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Price = price;
            NotificationDate = notificationDate;
            CategoryId = itemCategoryId;
            Deadline = deadline;
            IsBuyed = false;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
