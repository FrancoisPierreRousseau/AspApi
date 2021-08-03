using WebApi.Domain.Communication;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface IProductService<Model, Response> : IService<Model, Response>
        where Model : Product
        where Response : BaseResponse<Model>
    {
    }
}
