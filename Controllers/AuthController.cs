using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Domain.Security.Token;
using WebApi.Domain.Services;
using WebApi.Ressources;

namespace WebApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAuthenticationService authenticationService;

        public AuthController(IMapper mapper, IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            this.mapper = mapper;
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await this.authenticationService.CreateAccessTokenAsync(userCredentials.Email, userCredentials.Password);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var accessTokenResource = this.mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(accessTokenResource);
        }

        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await this.authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserEmail);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var tokenResource = this.mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(tokenResource);
        }

        [Route("/api/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenResource revokeTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
            return NoContent();
        }
    }
}
