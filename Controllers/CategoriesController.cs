using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Services;
using WebApi.Extensions;
using WebApi.Ressources;

namespace WebApi.Controllers
{
    [Route("/api")]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService<Category, CategoryResponse> categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService<Category, CategoryResponse> categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }


        [HttpGet("common/[controller]")]
       // [Authorize]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await this.categoryService.ListAsync();

            var resources = this.mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return resources;
        }

        [HttpPost("asministrator/[controller]")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = this.mapper.Map<SaveCategoryResource, Category>(resource);

            var result = await this.categoryService.SaveAsync(category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = this.mapper.Map<Category, CategoryResource>(result.model);
            return Ok(categoryResource);
        }

        [HttpPut("asministrator/[controller]/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = this.mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await this.categoryService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = this.mapper.Map<Category, CategoryResource>(result.model);
            return Ok(categoryResource);
        }

        [HttpDelete("asministrator/[controller]/{id}")]
      //  [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.categoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = this.mapper.Map<Category, CategoryResource>(result.model);
            return Ok(categoryResource);
        }
    }
}
