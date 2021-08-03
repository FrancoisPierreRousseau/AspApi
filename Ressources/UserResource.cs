using System.Collections.Generic;

namespace WebApi.Ressources
{
    public class UserResource : BaseRessource
    {
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
