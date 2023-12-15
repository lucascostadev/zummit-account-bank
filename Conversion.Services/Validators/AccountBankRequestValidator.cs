using Balance.Api.ViewModels.Convert;
using Balance.Domain.Interfaces;
using Balance.Services.Services;
using FluentValidation;

namespace Balance.Services.Validators
{
    public class AccountBankRequestValidator : AbstractValidator<AccountBankRequest>
    {
        public AccountBankRequestValidator(IAccountBankService accountBankService)
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();

            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();

            RuleFor(x => x).MustAsync(async (accountBank, cancellation) =>
            {
                bool exists = await accountBankService.Exists(accountBank.Email, accountBank.Id);
                return !exists;
            }).WithMessage("Existe conta já cadastrada com esse email.");

            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
