using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Balance.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<TEntity>> List() => await _databaseContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task Insert(TEntity obj)
        {
            await _databaseContext.Set<TEntity>().AddAsync(obj);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(TEntity obj)
        {
            _databaseContext.Entry(obj).State = EntityState.Modified;
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await Select(id);
            if (entity != null)
            {
                _databaseContext.Set<TEntity>().Remove(entity);
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task<IList<TEntity>> Select() => await _databaseContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> Select(int id) => await _databaseContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task InsertRange(IEnumerable<TEntity> list)
        {
            await _databaseContext.Set<TEntity>().AddRangeAsync(list);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
