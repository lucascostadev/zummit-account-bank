using Conversion.Domain.Entities;

namespace Conversion.Domain.Interfaces
{
    public interface IEuroService : IBaseService<Euro>
    {
        Task SyncWithEuroXref();

        Task<Euro?> GetWithCurrency(string currency);

        Task<decimal> Convert(string currencyTo, string currencyFrom, decimal value);
    }
}
