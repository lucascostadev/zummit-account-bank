using Balance.Domain.Interfaces;
using Balance.Infrastructure.CrossCutting;

namespace Balance.Infrastructure.Hangfire
{
    public class EuroXrefDailyJob
    {
        private readonly IEuroService _euroService;

        public EuroXrefDailyJob(IEuroService euroService)
        {
            _euroService = euroService;
        }

        public async Task Sync()
        {
            await _euroService.SyncWithEuroXref();
        }
    }
}
