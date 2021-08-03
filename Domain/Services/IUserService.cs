using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Models.Enums;

namespace WebApi.Domain.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}
