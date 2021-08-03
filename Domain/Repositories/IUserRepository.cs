using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Models.Enums;

namespace WebApi.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, ApplicationRole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}
