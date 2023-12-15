using Balance.Domain.Entities;
using FluentValidation;

namespace Balance.Services.Validators
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
