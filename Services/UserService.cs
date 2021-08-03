using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Models.Enums;
using WebApi.Domain.Repositories;
using WebApi.Domain.Security.Hashing;
using WebApi.Domain.Services;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordHasher passwordHasher;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            this.passwordHasher = passwordHasher;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public async Task<CreateUserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles)
        {
            var existingUser = await this.userRepository.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return new CreateUserResponse(false, "Email already in use.", null);
            }

            user.Password = this.passwordHasher.HashPassword(user.Password);

            await this.userRepository.AddAsync(user, userRoles);
            await this.unitOfWork.CompleteAsync();

            return new CreateUserResponse(true, null, user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await this.userRepository.FindByEmailAsync(email);
        }
    }
}
