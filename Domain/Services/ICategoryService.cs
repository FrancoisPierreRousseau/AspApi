using WebApi.Domain.Communication;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface ICategoryService<Model, Response> : IService<Model,Response>
        where Model : Category
        where Response : BaseResponse<Model>
    {
        
    }
}
