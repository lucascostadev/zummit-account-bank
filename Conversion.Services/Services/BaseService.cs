using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using FluentValidation;

namespace Conversion.Services.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual async Task<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(obj);
            return obj;
        }

        public virtual async Task Delete(int id) => await _baseRepository.Delete(id);

        public virtual async Task<IList<TEntity>> Get() => await _baseRepository.Select();

        public virtual async Task<TEntity?> GetById(int id) => await _baseRepository.Select(id);

        public virtual async Task<TEntity?> Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(obj);
            return obj;
        }
        
        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            validator.ValidateAndThrow(obj);
        }
    }
}
