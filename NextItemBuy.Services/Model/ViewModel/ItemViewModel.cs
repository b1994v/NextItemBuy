using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace NextItemBuy.Services.Model
{
    [Validator(typeof(ItemViewModelValidator))]
    public class ItemViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsBuyed { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class ItemViewModelValidator: AbstractValidator<ItemViewModel>
    {
        public ItemViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Required field.");
            RuleFor(x => x.Deadline)
                .NotNull()
                .WithMessage("Required field.");
            RuleFor(x => x.Price)
                .NotNull()
                .Must(x => x > 0)
                .WithMessage("Required field.");
            RuleFor(x => x.Deadline)
                .Must(x => x != null && x.Date > DateTime.MinValue && x.Date < DateTime.MaxValue.Date)
                .WithMessage("Required field.");
            RuleFor(x => x.NotificationDate)
                .Must(x => x != null && x.Date > DateTime.MinValue && x.Date < DateTime.MaxValue.Date)
                .WithMessage("Required field.");
        }
    }
}
