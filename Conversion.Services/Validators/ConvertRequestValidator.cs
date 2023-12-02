using Conversion.Api.ViewModels.Convert;
using Conversion.Domain.Interfaces;
using FluentValidation;

namespace Conversion.Services.Validators
{
    public class ConvertRequestValidator : AbstractValidator<ConvertRequest>
    {
        public ConvertRequestValidator(IEuroService euroService)
        {
            RuleFor(c => c.To).NotEmpty().NotNull().Custom((x, context) =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                if (x != "EURO" && euroService.GetWithCurrency(x).Result == null)
                {
                    context.AddFailure("", $"'To' not found {x}.");
                }
#pragma warning restore CS8604 // Possible null reference argument.
            });

            RuleFor(c => c.From).NotEmpty().NotNull().Custom((x, context) =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                if (x != "EURO" && euroService.GetWithCurrency(x).Result == null)
                {
                    context.AddFailure("", $"'From' not found {x}.");
                }
#pragma warning restore CS8604 // Possible null reference argument.
            });

            RuleFor(c => c.Value).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
