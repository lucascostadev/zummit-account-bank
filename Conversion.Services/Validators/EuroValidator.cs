using Conversion.Domain.Entities;
using FluentValidation;

namespace Conversion.Services.Validators
{
    public class EuroValidator : AbstractValidator<Euro>
    {
        public EuroValidator()
        {
            RuleFor(c => c.Currency).NotEmpty().NotNull();
            RuleFor(c => c.Value).NotEmpty().NotNull();
        }
    }
}
