using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace NextItemBuy.Services.Model.ViewModel
{
    [Validator(typeof(BankViewModelValidator))]
    public class BankViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Budget { get; set; }
        public bool IsIncome { get; set; }
        public string Reason { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class BankViewModelValidator: AbstractValidator<BankViewModel>
    {
        public BankViewModelValidator()
        {
            RuleFor(x => x.Budget)
                .Must(x => x != 0)
                .WithMessage("Required field.");
            RuleFor(x => x.Reason)
                .NotEmpty()
                .When(x => !x.IsIncome)
                .WithMessage("Required field.");
        }
    }
}
