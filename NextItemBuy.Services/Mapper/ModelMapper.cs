using NextItemBuy.Domain;
using NextItemBuy.Services.Model;
using NextItemBuy.Services.Model.ViewModel;

namespace NextItemBuy.Services.Mapper
{
    public static class ModelMapper
    {
        public static UserModel ToViewModel(this User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                UserName = entity.Username,
                Image = entity.Image
            };
        }

        public static ItemViewModel ToViewModel(this Item entity)
        {
            return new ItemViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Deadline = entity.Deadline,
                NotificationDate = entity.NotificationDate,
                CategoryId = entity.ItemCategory.Id,
                CategoryName = entity.ItemCategory.CategoryName,
                Price = entity.Price,
                IsBuyed = entity.IsBuyed,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static BankViewModel ToViewModel(this Bank entity)
        {
            return new BankViewModel
            {
                Id = entity.Id,
                Budget = entity.Budget,
                IsIncome = entity.IsIncome,
                Reason = entity.Reason,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static ItemCategoryViewModel ToViewModel(this ItemCategory entity)
        {
            return new ItemCategoryViewModel
            {
                CategoryId = entity.Id,
                CategoryName = entity.CategoryName
            };
        }
    }
}
