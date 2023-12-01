using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Conversion.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _databaseContext;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

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

        public async Task<IList<TEntity>> Select() => await _databaseContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> Select(int id) => await _databaseContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

    }
}
