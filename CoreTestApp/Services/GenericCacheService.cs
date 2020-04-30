using CoreTestApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Services
{

    public class GenericCacheService<TEntity> where TEntity:class
    {
        private TestAppDbContext _context;
        private IMemoryCache _cache;

        public GenericCacheService(TestAppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }


        public IQueryable<TEntity> GetAll(bool cacheInclude = false)
        {
            if (!cacheInclude)
            {
                return _context.Set<TEntity>();
            }

            if (!_cache.TryGetValue(typeof(TEntity).GUID, out IEnumerable<TEntity> tempObj))
            {
                tempObj = _context.Set<TEntity>().ToList();
                
                if (tempObj != null)
                {
                   _cache.Set(typeof(TEntity).GUID, tempObj, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                }
            }
            return tempObj.AsQueryable();
        }

        public async Task<TEntity> GetById(string id)
        {
            TEntity item = null;
            if (!_cache.TryGetValue(id, out item))
            {
                item = await _context.Set<TEntity>().FindAsync(id);

                if (item != null)
                {
                    _cache.Set(id, item, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                }
            }
           
            return item;
        }
    }
}
