using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Services;
using WebApi.Ressources;

namespace WebApi.Controllers
{
    [Route("/api")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService<Product, ProductResponse> productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService<Product, ProductResponse> productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet("common/[controller]")]
        [Authorize]
        public async Task<IEnumerable<ProductResource>> ListAsync()
        {
            var products = await productService.ListAsync();


            var resources = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return resources;
        }
    }
}
