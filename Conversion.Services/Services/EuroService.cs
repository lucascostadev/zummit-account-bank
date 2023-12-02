using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.CrossCutting;
using Conversion.Infrastructure.Data.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace Conversion.Services.Services
{
    public class EuroService : BaseService<Euro>, IEuroService
    {
        private readonly EuroXrefDailyService _euroXrefDailyService;
        private readonly IEuroRepository _euroRepository;
        private readonly IMemoryCache _cache;

        public EuroService(IBaseRepository<Euro> baseRepository,
                           IEuroRepository euroRepository,
                           EuroXrefDailyService euroXrefDailyService,
                           IMemoryCache cache) : base(baseRepository)
        {
            _euroXrefDailyService = euroXrefDailyService;
            _euroRepository = euroRepository;
            _cache = cache;
        }

        //public async Euro? GetByCurrency(string currency) => await _baseRepository.List

        public async Task SyncWithEuroXref()
        {
            var obj = await _euroXrefDailyService.Get();

            if (obj != null && obj.Cube != null && obj.Cube.Cubes != null && obj.Cube.Cubes.Length > 0)
            {
                foreach (var cube in obj.Cube.Cubes)
                {
                    await this.AddRange(cube.Cubes.Select(x => new Euro(x.CurrencyCode, x.Rate)));
                }
            }
        }

        public async Task AddRange(IEnumerable<Euro> entities) => await _euroRepository.InsertRange(entities);

        public async Task<Euro?> GetWithCurrency(string currency)
        {
            var cacheEntry = _cache.GetOrCreateAsync($"{currency}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1) - DateTime.Now;
                entry.SetPriority(CacheItemPriority.High);

                return _euroRepository.GetByCurrency(currency);
            });

            return (await cacheEntry);
        }

        public async Task<decimal> Convert(string currencyTo, string currencyFrom, decimal value)
        {
            var cacheEntry = _cache.GetOrCreate($"{currencyTo}-{currencyFrom}-{value}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1) - DateTime.Now;
                entry.SetPriority(CacheItemPriority.High);

                return ConvertL(currencyTo, currencyFrom, value);
            });

            return (await cacheEntry);
        }

        public async Task<decimal> ConvertL(string currencyTo, string currencyFrom, decimal value)
        {
            var to = currencyTo == "EURO" ? 1m : (decimal)(await GetWithCurrency(currencyTo))?.Value;
            var from = (currencyFrom == "EURO" ? 1m : (decimal)(await GetWithCurrency(currencyFrom))?.Value);

            if (currencyTo == "EURO")
            {
                return value * from;
            }

            if (currencyFrom == "EURO")
            {
                return value / to;
            }

            return (value / to) * from;
        }
    }
}
