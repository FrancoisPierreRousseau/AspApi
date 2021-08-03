using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Domain.Models;
using WebApi.Domain.Models.Enums;
using WebApi.Domain.Services;
using WebApi.Ressources;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = this.mapper.Map<UserCredentialsResource, User>(userCredentials);

            var response = await this.userService.CreateUserAsync(user, ApplicationRole.Common);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = this.mapper.Map<User, UserResource>(response.User);
            return Ok(userResource);
        }
    }
}
