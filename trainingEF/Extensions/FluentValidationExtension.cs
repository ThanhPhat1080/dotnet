using FluentValidation.AspNetCore;
using FluentValidation;
using trainingEF.Models.DTOs;
using trainingEF.Models.Validators;

namespace trainingEF.Extensions;

public static class FluentValidationExtension
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection service)
    {
        service.AddFluentValidationAutoValidation();
        service.AddValidatorsFromAssemblyContaining<UserRegistrationRequestValidator>();
        service.AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>();
        return service;
    }
}
