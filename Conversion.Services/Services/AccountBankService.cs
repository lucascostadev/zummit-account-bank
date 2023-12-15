using Balance.Domain.Entities;
using Balance.Domain.Interfaces;

namespace Balance.Services.Services
{
    public class AccountBankService : BaseService<AccountBank>, IAccountBankService
    {
        public AccountBankService(IBaseRepository<AccountBank> baseRepository) : base(baseRepository)
        {
        }

        public async Task<bool> Exists(string email, int id)
        {
            return (await baseRepository.List()).Any(x=>x.Id != id && x.Email == email);
        }
    }
}
