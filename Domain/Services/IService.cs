using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface IService<Model, Response>
        where Model: AbstractModel
        where Response: BaseResponse<Model>
    {
        Task<IEnumerable<Model>> ListAsync();
        Task<BaseResponse<Model>> SaveAsync(Model model);
        Task<BaseResponse<Model>> UpdateAsync(int id, Model model);
        Task<BaseResponse<Model>> DeleteAsync(int id);

    }
}
