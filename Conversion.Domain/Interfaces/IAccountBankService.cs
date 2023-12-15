using Balance.Domain.Entities;

namespace Balance.Domain.Interfaces
{
    public interface IAccountBankService : IBaseService<AccountBank>
    {
        Task<bool> Exists(string email, int id);
    }
}
