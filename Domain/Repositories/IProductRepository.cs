using WebApi.Domain.Models;

namespace WebApi.Domain.Repositories
{
    public interface IProductRepository<Model> : IRepository<Model>
        where Model : Product
    {
    }
}
