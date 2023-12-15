using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Balance.Infrastructure.Data.Repository
{
    public class EuroRepository : BaseRepository<Euro>, IEuroRepository
    {
        public EuroRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<Euro?> GetByCurrency(string currency) => await _databaseContext.Euro.OrderBy(x => x.CreatedAt).LastOrDefaultAsync(x => x.Currency == currency);

    }
}
