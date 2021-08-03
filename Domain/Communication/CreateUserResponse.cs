using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Models;

namespace WebApi.Domain.Communication
{
    public class CreateUserResponse : BaseResponse<AbstractModel>
    {
        public User User { get; private set; }

        public CreateUserResponse(bool success, string message, User user) : base(success, message, user)
        {  
        }
    }
}
