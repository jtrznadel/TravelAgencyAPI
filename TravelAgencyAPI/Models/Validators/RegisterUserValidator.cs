using FluentValidation;
using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(TravelAgencyDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .MinimumLength(7);
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if(emailInUse)
                {
                    context.AddFailure("Email", "Email is already taken");
                }
            });
                
        }
    }
}
