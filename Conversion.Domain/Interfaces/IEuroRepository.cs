using Conversion.Domain.Entities;

namespace Conversion.Domain.Interfaces
{
    public interface IEuroRepository : IBaseRepository<Euro>
    {
        Task<Euro?> GetByCurrency(string currency);
    }
}
