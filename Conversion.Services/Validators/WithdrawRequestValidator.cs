using Balance.Domain.Interfaces;
using Balance.Domain.ViewModels.AccountBank;
using Balance.Services.Services;
using FluentValidation;

namespace Balance.Services.Validators
{
    public class WithdrawRequestValidator : AbstractValidator<WithdrawRequest>
    {
        public WithdrawRequestValidator(IAccountBankService accountBankService)
        {

            RuleFor(x => x.WithdrawValue).NotEmpty().NotNull().GreaterThan(decimal.Zero);

            RuleFor(x => x).CustomAsync((async (withdraw, context, cancellation) =>
            {
                if (!await accountBankService.CanWithdraw(withdraw))
                {
                    context.AddFailure("WithdrawValue", $"Não existe saldo disponível para sacar esse valor.");
                }
            }));
        }
    }
}
