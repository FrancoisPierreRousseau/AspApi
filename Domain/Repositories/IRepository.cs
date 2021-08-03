using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Models;

namespace WebApi.Domain.Repositories
{
    public interface IRepository<Model> where Model : AbstractModel
    {
        Task<IEnumerable<Model>> ListAsync();
        Task AddAsync(Model category);
        Task<Model> FindByIdAsync(int id);
        void Update(Model category);
        void Remove(Model category);
    }
}
