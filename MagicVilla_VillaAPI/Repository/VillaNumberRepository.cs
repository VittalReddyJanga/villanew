using MagicVilla_VillaAPI.DATA;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContect _db;
        public VillaNumberRepository(ApplicationDbContect db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;

        }
    }
}
