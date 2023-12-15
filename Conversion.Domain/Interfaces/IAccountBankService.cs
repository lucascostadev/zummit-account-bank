using Balance.Domain.Entities;
using Balance.Domain.ViewModels.AccountBank;

namespace Balance.Domain.Interfaces
{
    public interface IAccountBankService : IBaseService<AccountBank>
    {
        Task<bool> Exists(string email, int id);

        Task<AccountBank> Deposit(int accountBankId, DepositRequest deposit);

        Task<AccountBank> Withdraw(int accountBankId, WithdrawRequest model);

        Task<bool> CanWithdraw(WithdrawRequest withdraw);
    }
}
