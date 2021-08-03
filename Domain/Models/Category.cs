using System.Collections.Generic;

namespace WebApi.Domain.Models
{
    public class Category : AbstractModel
    {
        public string Name { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
