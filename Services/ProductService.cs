
using System;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;

namespace WebApi.Services
{
    public class ProductService : AbstractService<BaseResponse<Product>, Product>, IProductService<Product, ProductResponse>
    {
        public ProductService(IProductRepository<Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork, typeof(ProductResponse))
        {
        }
    }
}
