using AutoMapper;
using WebApi.Domain.Models;
using WebApi.Ressources;

namespace WebApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();

            CreateMap<UserCredentialsResource, User>();
        }
    }
}
