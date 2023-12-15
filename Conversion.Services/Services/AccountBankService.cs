using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using Balance.Domain.ViewModels.AccountBank;

namespace Balance.Services.Services
{
    public class AccountBankService : BaseService<AccountBank>, IAccountBankService
    {
        public AccountBankService(IBaseRepository<AccountBank> baseRepository) : base(baseRepository)
        {
        }

        public async Task<bool> Exists(string email, int id)
        {
            return (await baseRepository.List()).Any(x => x.Id != id && x.Email == email);
        }

        public async Task<AccountBank> Deposit(int accountBankId, DepositRequest deposit)
        {
            var accountBank = await GetById(accountBankId);

            if (accountBank == null)
            {
                throw new Exception("Não foi encontrado nenhuma conta bancária com esse ID.");
            }

            accountBank.Balance += deposit.DepositValue.Value;

            return await this.Update(accountBank);
        }

        public async Task<bool> CanWithdraw(WithdrawRequest withdraw)
        {
            var accountBank = await GetById(withdraw.AccountBankId);

            if (accountBank == null)
            {
                throw new Exception("Não foi encontrado nenhuma conta bancária com esse ID.");
            }

            return accountBank.CanWithdraw(withdraw.WithdrawValue.Value);
        }

        public async Task<AccountBank> Withdraw(int accountBankId, WithdrawRequest withdraw)
        {
            var accountBank = await GetById(withdraw.AccountBankId);

            if (accountBank == null)
            {
                throw new Exception("Não foi encontrado nenhuma conta bancária com esse ID.");
            }

            if (accountBank.CanWithdraw(withdraw.WithdrawValue.Value))
            {
                accountBank.Balance -= withdraw.WithdrawValue.Value;

                accountBank = await this.Update(accountBank);
            }

            return accountBank;
        }
    }
}
