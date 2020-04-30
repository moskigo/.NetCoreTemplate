using CoreTestApp.Model;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Extensions
{
    public static class EntityExtension
    {
        public static IQueryable<WorkerViewModel> GetViewModel(this IQueryable<Worker> workers) => workers.Select(a => 
        new WorkerViewModel
        {
            ID = a.Id,
            Company = a.Company,
            CompanyId = a.CompanyId,
            Email = a.Email,
            Name = a.Name,
            RowVersion = a.ConcurrencyStamp
        });

        
    }
}
