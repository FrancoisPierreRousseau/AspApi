using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;

namespace WebApi.Services
{
    public abstract class AbstractService<Response, Model> : IService<Model, Response>
        where Model: AbstractModel
        where Response: BaseResponse<Model>
    {
        private readonly IRepository<Model> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly Type responseType;

        protected AbstractService(IRepository<Model> repository, IUnitOfWork unitOfWork, Type responseType)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.responseType = responseType;
        }

        public async Task<BaseResponse<Model>> DeleteAsync(int id)
        {
            var existingModel = await this.repository.FindByIdAsync(id);
           
            if (existingModel == null)
                return Activator.CreateInstance(this.responseType, new object[] { "Category not found" }) as Response;

            try
            {
                this.repository.Remove(existingModel);
                await this.unitOfWork.CompleteAsync();
                return Activator.CreateInstance(this.responseType, new object[] { existingModel }) as Response;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return Activator.CreateInstance(this.responseType, new object[] { $"An error occurred when deleting the category: {ex.Message}" }) as Response;
            }
        }

        public async Task<IEnumerable<Model>> ListAsync()
        {
            return await this.repository.ListAsync();
        }

        public async Task<BaseResponse<Model>> SaveAsync(Model model)
        {
            try
            {
                await this.repository.AddAsync(model as Model);
                await this.unitOfWork.CompleteAsync();

                return Activator.CreateInstance(this.responseType, new object[] { model }) as Response;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return Activator.CreateInstance(this.responseType, new object[] { $"An error occurred when saving the category: {ex.Message}" }) as Response;
            }
        }



        public async Task<BaseResponse<Model>> UpdateAsync(int id, Model model)
        {
            var existingModel = await this.repository.FindByIdAsync(id);

            if (existingModel == null)
                return Activator.CreateInstance(this.responseType, new object[] { "Category not found." }) as Response;

            try
            {
                this.repository.Update(existingModel);
                await this.unitOfWork.CompleteAsync();
                return Activator.CreateInstance(this.responseType, new object[] { existingModel }) as Response;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return Activator.CreateInstance(this.responseType, new object[] { $"An error occurred when updating the category: {ex.Message}" }) as Response;
            }
        }

    }
}
