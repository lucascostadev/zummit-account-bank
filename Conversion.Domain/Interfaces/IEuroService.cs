using Conversion.Domain.Entities;

namespace Conversion.Domain.Interfaces
{
    public interface IEuroService : IBaseService<Euro>
    {
        Task SyncWithEuroXref();
    }
}
