using Balance.Domain.Entities;

namespace Balance.Domain.Interfaces
{
    public interface IEuroRepository : IBaseRepository<Euro>
    {
        Task<Euro?> GetByCurrency(string currency);
    }
}
