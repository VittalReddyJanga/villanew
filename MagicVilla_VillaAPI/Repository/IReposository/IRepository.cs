using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IReposository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, string? includeproperty =null);
        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeproperty = null);
        Task Remove(T entity);
        Task Create(T entity);
        Task Save();
    }
}
