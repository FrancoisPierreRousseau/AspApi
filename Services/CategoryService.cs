using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;

namespace WebApi.Services
{
    public class CategoryService : AbstractService<BaseResponse<Category>, Category>, ICategoryService<Category, CategoryResponse >
    {
        public CategoryService(ICategoryRespository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork, typeof(CategoryResponse))
        {
        }
    }
}
