using Balance.Domain.Interfaces;
using Balance.Domain.ViewModels.AccountBank;
using FluentValidation;

namespace Balance.Services.Validators
{
    public class DepostitRequestValidator : AbstractValidator<DepositRequest>
    {
        public DepostitRequestValidator()
        {

            RuleFor(x => x.DepositValue).NotEmpty().NotNull().GreaterThan(decimal.Zero);
        }
    }
}
