using WebApi.Domain.Models.Enums;

namespace WebApi.Domain.Models
{
    public class Product : AbstractModel
    {
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
