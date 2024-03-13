using MagicVilla_Web.Models.DTOS;
using System.Linq.Expressions;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateDto dto);       
        Task<T> UpdateAsync<T>(VillaUpdateDto dto);
    }
}
