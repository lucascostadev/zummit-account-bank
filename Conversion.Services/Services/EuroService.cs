using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.CrossCutting;
using Conversion.Infrastructure.Data.Repository;

namespace Conversion.Services.Services
{
    public class EuroService : BaseService<Euro>, IEuroService
    {
        private readonly EuroXrefDailyService _euroXrefDailyService;
        private readonly IEuroRepository _euroRepository;

        public EuroService(IBaseRepository<Euro> baseRepository, IEuroRepository euroRepository, EuroXrefDailyService euroXrefDailyService) : base(baseRepository)
        {
            _euroXrefDailyService = euroXrefDailyService;
            _euroRepository = euroRepository;
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

        public async Task<Euro?> GetWithCurrency(string currency) => await _euroRepository.GetByCurrency(currency);

    }
}
