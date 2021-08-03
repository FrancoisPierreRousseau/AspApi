using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Repositories;
using WebApi.Persistences.Contexts;

namespace WebApi.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
