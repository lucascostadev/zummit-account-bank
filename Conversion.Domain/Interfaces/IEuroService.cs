using Balance.Domain.Entities;

namespace Balance.Domain.Interfaces
{
    public interface IEuroService : IBaseService<Euro>
    {
        Task SyncWithEuroXref();

        Task<Euro?> GetWithCurrency(string currency);

        Task<decimal> Convert(string currencyTo, string currencyFrom, decimal value);
    }
}
