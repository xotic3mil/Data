using Business.Dtos;
using Business.Models;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IBaseService<TEntity, TDto, TModel>
    {
        public Task<TModel> CreateAsync(TDto form);

        public Task<TModel> GetAsyncID(int id);

        public Task<IEnumerable<TModel>> GetAllAsync(string? search);

        public Task<TModel> UpdateAsync(TModel model);

        public Task<bool> DeleteAsync(int id);
    }
}