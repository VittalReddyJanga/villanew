using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI.Repository.IReposository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> Update(VillaNumber entity);
    }
}
