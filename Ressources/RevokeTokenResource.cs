using System.ComponentModel.DataAnnotations;

namespace WebApi.Ressources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}
