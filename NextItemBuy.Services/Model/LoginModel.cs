using FluentValidation;
using FluentValidation.Attributes;

namespace NextItemBuy.Services.Model
{
    [Validator(typeof(LoginModelValidator))]
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelValidator: AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Required field.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Required field.");
        }
    }
}
