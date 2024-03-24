using Blog.Entity.Entities;
using FluentValidation;

namespace Blog.Business.FluentValidations
{
    public class UserValidator:AbstractValidator<AppUser>
    {
        public UserValidator()
        {
          RuleFor(x=>x.FirstName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithName("İsim");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(50)
               .WithName("Soy İsim");

            RuleFor(x => x.PhoneNumber)
               .NotEmpty()
               .NotNull()
               .MinimumLength(11)
               .WithName("Telefon Numarası");

        }
    }
}

