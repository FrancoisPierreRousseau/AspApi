using AutoMapper;
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
    public class AbstractController : ControllerBase
    {
        private readonly IService<AbstractModel, BaseResponse<AbstractModel>> service;
        private readonly IMapper mapper;

        public AbstractController(IService<AbstractModel, BaseResponse<AbstractModel>> service, IMapper mapper)
        {       
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BaseRessource>> GetAllAsync(IService<AbstractModel, BaseResponse<AbstractModel>> service)
        {
            var models = await service.ListAsync();

            var resources = this.mapper.Map<IEnumerable<AbstractModel>, IEnumerable<BaseRessource>>(models);

            return resources;
        }


        public async Task<IActionResult> PostAsync([FromBody] AbstractSaveRessource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = this.mapper.Map<AbstractSaveRessource, AbstractModel>(resource);

            var result = await this.service.SaveAsync(category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = this.mapper.Map<AbstractModel, BaseRessource>(result.model);
            return Ok(categoryResource);
        }


        public async Task<IActionResult> PutAsync(int id, [FromBody] AbstractSaveRessource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = this.mapper.Map<AbstractSaveRessource, Category>(resource);
            var result = await this.service.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = this.mapper.Map<AbstractModel, BaseRessource>(result.model);
            return Ok(categoryResource);
        }


        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = this.mapper.Map<AbstractModel, BaseRessource>(result.model);
            return Ok(categoryResource);
        }
        
    }
}
