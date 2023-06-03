using NextItemBuy.Domain;
using NextItemBuy.Services.Exceptions;
using NextItemBuy.Services.Interfaces;
using NextItemBuy.Services.Mapper;
using NextItemBuy.Services.Model;
using NextItemBuy.Services.Model.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace NextItemBuy.Services.Implementation
{
    public class ItemsService : IItemsService
    {
        public List<ItemViewModel> LoadItems(ItemsSearchModel searchModel, out int total)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var query = ctx.Items.AsQueryable();

                if (!string.IsNullOrEmpty(searchModel.Key))
                {
                    query = query.Where(x => x.Name.Contains(searchModel.Key) || x.Description.Contains(searchModel.Key));
                }
                if (searchModel.Active.HasValue)
                {
                    if (searchModel.Active.Value)
                    {
                        query = query.Where(x => x.IsBuyed);
                    }
                    if (!searchModel.Active.Value)
                    {
                        query = query.Where(x => !x.IsBuyed);
                    }
                }

                total = query.Count();
                
                return query.OrderByDescending(x => x.Deadline)
                            .Skip((searchModel.Page - 1) * searchModel.PageSize)
                            .Take(searchModel.PageSize)
                            .ToList()
                            .Select(x => x.ToViewModel())
                            .ToList();
            }
        }

        public ItemViewModel LoadItemDetails(int itemId)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var item = ctx.Items.FirstOrDefault(x => x.Id == itemId);
                if(item == null)
                {
                    throw new ApplicationException("Item not found");
                }

                return item.ToViewModel();
            }
        }

        public void SaveOrUpdate(ItemViewModel model, IPrincipal user)
        {
            var validator = new ItemViewModelValidator();
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

                var item = ctx.Items.FirstOrDefault(x => x.Id == model.Id);

                if(item != null)
                {
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.Price = model.Price;
                    item.NotificationDate = model.NotificationDate;
                    item.Deadline = model.Deadline;
                    item.IsBuyed = model.IsBuyed;
                    item.ModifiedOn = DateTime.Now;

                    ctx.SaveChanges();
                    return;
                }

                item = new Item(userModel.Id, model.Name, model.Description, model.Price, model.Deadline, model.NotificationDate);
                ctx.Items.Add(item);

                ctx.SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            using(var ctx = new NextItemBuyEntities())
            {
                var item = ctx.Items.FirstOrDefault(x => x.Id == itemId);
                if (item == null)
                {
                    throw new ApplicationException("Item not found");
                }

                ctx.Items.Remove(item);
                ctx.SaveChanges();
            }
        }

        public void SetItemToBuyed(int itemId)
        {
            using (var ctx = new NextItemBuyEntities())
            {
                var item = ctx.Items.FirstOrDefault(x => x.Id == itemId);
                if (item == null)
                {
                    throw new ApplicationException("Item not found");
                }

                item.IsBuyed = true;

                ctx.SaveChanges();
            }
        }
    }
}
