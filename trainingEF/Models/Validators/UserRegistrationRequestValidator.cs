namespace trainingEF.Models.Validators;
using FluentValidation;
using trainingEF.Models.DTOs;

public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequestDto>
{
    public UserRegistrationRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Minimum Name to create profile is 5")
            .MaximumLength(15).WithMessage("Maximum Name to create profile is 15");
        RuleFor(p => p.Email)
            .EmailAddress();
        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Minimum age to create profile is 10");
    }
}
