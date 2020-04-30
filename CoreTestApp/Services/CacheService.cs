using CoreTestApp.Extensions;
using CoreTestApp.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Services
{
    public class CacheService
    {
        private IMemoryCache Cache;
        private TestAppDbContext Context;

        public CacheService(IMemoryCache cache, TestAppDbContext context)
        {
            this.Cache = cache;
            this.Context = context;
        }

        public WorkerViewModel GetWorkerById(string id)
        {
            WorkerViewModel worker = null;
            if (!Cache.TryGetValue(id, out worker))
            {
                worker = Context.Workers.GetViewModel().Where(a => a.ID == id).FirstOrDefault();

                if (worker != null)
                {
                    Cache.Set(worker.ID, worker, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                }
            }

            return worker;
        }
    }
}
