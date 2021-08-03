using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Models;

namespace WebApi.Domain.Repositories
{
    public interface ICategoryRespository<Model> : IRepository<Model>
         where Model : Category
    {

    }
}
