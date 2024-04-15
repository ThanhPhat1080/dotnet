using FluentValidation;
using trainingEF.Models.DTOs;

namespace trainingEF.Models.Validators;

public class UserLoginRequestValidator : UserRegistrationRequestValidator
{
    public UserLoginRequestValidator()
    {
        RuleFor(p => p.Email)
            .EmailAddress();
        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Minimum lengh of password is 5");
    }
}
