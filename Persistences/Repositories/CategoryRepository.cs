using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Persistences.Contexts;

namespace WebApi.Persistences.Repositories
{
    public class CategoryRepository : AbstractRepository<Category>, ICategoryRespository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context){ }
    }
}
