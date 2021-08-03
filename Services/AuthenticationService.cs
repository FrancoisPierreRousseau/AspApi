using System.Threading.Tasks;
using WebApi.Domain.Communication;
using WebApi.Domain.Security.Hashing;
using WebApi.Domain.Security.Token;
using WebApi.Domain.Services;

namespace WebApi.Services
{
    public class AuthenticationService: Domain.Services.IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenHandler tokenHandler;

        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            this.tokenHandler = tokenHandler;
            this.passwordHasher = passwordHasher;
            this.userService = userService;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            var user = await this.userService.FindByEmailAsync(email);

            if (user == null || !this.passwordHasher.PasswordMatches(password, user.Password))
            {
                return new TokenResponse(false, "Invalid credentials.", null);
            }

            var token = this.tokenHandler.CreateAccessToken(user);

            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail)
        {
            var token = this.tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            if (token.IsExpired())
            {
                return new TokenResponse(false, "Expired refresh token.", null);
            }

            var user = await this.userService.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            var accessToken = this.tokenHandler.CreateAccessToken(user);
            return new TokenResponse(true, null, accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            this.tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}
