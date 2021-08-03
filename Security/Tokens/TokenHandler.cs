using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using WebApi.Domain.Models;
using WebApi.Domain.Security.Hashing;
using WebApi.Domain.Security.Token;

namespace WebApi.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> refreshTokens = new HashSet<RefreshToken>();

        private readonly TokenOptions tokenOptions;
        private readonly SigningConfigurations signingConfigurations;
        private readonly IPasswordHasher passwordHaser;

        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHaser)
        {
            this.passwordHaser = passwordHaser;
            this.tokenOptions = tokenOptionsSnapshot.Value;
            this.signingConfigurations = signingConfigurations;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var refreshToken = BuildRefreshToken();
            var accessToken = BuildAccessToken(user, refreshToken);
            this.refreshTokens.Add(refreshToken);

            return accessToken;
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var refreshToken = this.refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                this.refreshTokens.Remove(refreshToken);

            return refreshToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        private RefreshToken BuildRefreshToken()
        {
            var refreshToken = new RefreshToken
            (
                token: this.passwordHaser.HashPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddSeconds(this.tokenOptions.RefreshTokenExpiration).Ticks
            );

            return refreshToken;
        }

        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(this.tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                issuer: this.tokenOptions.Issuer,
                audience: this.tokenOptions.Audience,
                claims: GetClaims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: this.signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            return claims;
        }
    }
}
