using Balance.Domain.Entities;
using Balance.Domain.Interfaces;

namespace Balance.Infrastructure.Data.Repository
{
    public class AccountBankRepository : BaseRepository<AccountBank>, IAccountBankRepository
    {
        public AccountBankRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
