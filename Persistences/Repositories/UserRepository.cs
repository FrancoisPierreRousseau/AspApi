using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Models;
using WebApi.Domain.Models.Enums;
using WebApi.Domain.Repositories;
using WebApi.Persistences.Contexts;

namespace WebApi.Persistences.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(User user, ApplicationRole[] userRoles)
        {
            var roleNames = userRoles.Select(r => r.ToString()).ToList();
            var roles = await this.context.Roles.Where(r => roleNames.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            this.context.Users.Add(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await this.context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
