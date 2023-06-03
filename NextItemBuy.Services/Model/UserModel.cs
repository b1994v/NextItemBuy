using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace NextItemBuy.Services.Model
{
    [Validator(typeof(UserModelValidator))]
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class UserModelValidator: AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Required field.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Required field.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Required field.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Required field.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Required field.");

        }
    }
}
