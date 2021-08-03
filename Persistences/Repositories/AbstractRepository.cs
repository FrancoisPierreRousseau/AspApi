using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Extensions;

namespace WebApi.Persistences.Repositories
{
    public abstract class AbstractRepository<Model> : IRepository<Model>
        where Model : AbstractModel
    {
        protected readonly DbContext context;

        protected DbSet<Model> entities;

        public AbstractRepository(DbContext context)
        {
            this.context = context;
            this.entities = this.context.Set<Model>();
        }

        public async Task AddAsync(Model model)
        {
            await this.entities.AddAsync(model);
        }

        public async Task<Model> FindByIdAsync(int id)
        {
            return await this.entities.FindAsync(id);
        }

        public async Task<IEnumerable<Model>> ListAsync()
        {
            return await this.entities.MultiInclude(typeof(AbstractModel)).ToListAsync();
        }

        public void Remove(Model model)
        {
            this.entities.Remove(model);
        }

        public void Update(Model model)
        {
            this.entities.Update(model);
        }
    }
}
