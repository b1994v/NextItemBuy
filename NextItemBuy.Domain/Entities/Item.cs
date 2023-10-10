using System;

namespace NextItemBuy.Domain
{
    public partial class Item
    {
        public Item()
        {

        }
        public Item(Guid userId, string name, string description, decimal price, int itemCategoryId, DateTime deadline, DateTime notificationDate, bool isBuyed)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Price = price;
            NotificationDate = notificationDate;
            CategoryId = itemCategoryId;
            Deadline = deadline;
            IsBuyed = isBuyed;
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
    }
}
