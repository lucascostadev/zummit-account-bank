using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.CrossCutting;
using Conversion.Infrastructure.Data.Repository;

namespace Conversion.Services.Services
{
    public class EuroService : BaseService<Euro>, IEuroService
    {
        private readonly EuroXrefDailyService _euroXrefDailyService;
        private readonly IBaseRepository<Euro> _baseRepository;

        public EuroService(IBaseRepository<Euro> baseRepository, EuroXrefDailyService euroXrefDailyService) : base(baseRepository)
        {
            _euroXrefDailyService = euroXrefDailyService;
            _baseRepository = baseRepository;
        }

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

        public async Task AddRange(IEnumerable<Euro> entities) => await _baseRepository.InsertRange(entities);
    }
}
