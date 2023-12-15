using Balance.Domain.Entities;
using Balance.Domain.Interfaces;
using FluentValidation;

namespace Balance.Services.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        public readonly IBaseRepository<TEntity> baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public virtual async Task<TEntity> Add(TEntity obj)
        {
            await baseRepository.Insert(obj);
            return obj;
        }

        public virtual async Task Delete(int id) => await baseRepository.Delete(id);

        public virtual async Task<IList<TEntity>> Get() => await baseRepository.Select();

        public virtual async Task<TEntity?> GetById(int id) => await baseRepository.Select(id);

        public virtual async Task<TEntity?> Update(TEntity obj)
        {
            await baseRepository.Update(obj);
            return obj;
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            validator.ValidateAndThrow(obj);
        }
    }
}
