using MagicVilla_VillaAPI.DATA;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContect _db;

        public VillaRepository(ApplicationDbContect db) :base(db)
        {
            _db = db;
        }

        public async Task<Villa> Update(Villa entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
