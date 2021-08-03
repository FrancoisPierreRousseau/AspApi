using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Security.Token;

namespace WebApi.Domain.Communication
{
    public class TokenResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public AccessToken Token { get; set; }

        public TokenResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public TokenResponse(bool success, string message, AccessToken token) : this(success, message) => Token = token;
    }
}
