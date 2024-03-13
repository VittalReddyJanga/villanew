using MagicVilla_VillaAPI.DATA;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IReposository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContect _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContect db)
        {
            _db = db;
           // _db.VillaNumbers.Include(u => u.Villa).ToList();
            _dbSet = _db.Set<T>();
        }

        //"villa, villaSpecial"
        public async Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeproperty = null)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if(includeproperty != null)
            {
                foreach(var property in includeproperty.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeproperty =null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeproperty != null)
            {
                foreach (var property in includeproperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
        }

        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
