using Balance.Domain.Entities;

namespace Balance.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity obj);

        Task InsertRange(IEnumerable<TEntity> list);

        Task Update(TEntity obj);

        Task Delete(int id);

        Task<IList<TEntity>> Select();

        Task<TEntity?> Select(int id);

        Task<IEnumerable<TEntity>> List();
    }
}
