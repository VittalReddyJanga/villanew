using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IReposository
{
    public interface IVillaRepository : IRepository<Villa>
    {        
         Task<Villa> Update(Villa entity);
              
    }
}
