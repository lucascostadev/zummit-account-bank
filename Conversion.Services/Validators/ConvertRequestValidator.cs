using Conversion.Api.ViewModels.Convert;
using Conversion.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Conversion.Services.Validators
{
    public class ConvertRequestValidator : AbstractValidator<ConvertRequest>
    {
        public ConvertRequestValidator(IEuroService euroService)
        {
            RuleFor(c => c.To).NotEmpty().NotNull().Custom((x, context) =>
            {
                if (euroService.GetWithCurrency(x).Result == null)
                {
                    context.AddFailure("", $"We can´t find {x}.");
                }
            });

            RuleFor(c => c.From).NotEmpty().NotNull().Custom((x, context) =>
            {
                if (euroService.GetWithCurrency(x).Result == null)
                {
                    context.AddFailure("", $"We can´t find {x}.");
                }
            });

            RuleFor(c => c.Value).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
