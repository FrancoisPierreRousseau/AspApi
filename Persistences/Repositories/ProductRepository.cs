using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Persistences.Contexts;

namespace WebApi.Persistences.Repositories
{
    public class ProductRepository : AbstractRepository<Product>, IProductRepository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context){ }
    }
}
