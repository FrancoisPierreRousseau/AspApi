namespace WebApi.Ressources
{
    public class ProductResource : BaseRessource
    {
        public string Name { get; set; }
        public int QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public CategoryResource Category { get; set; }
    }
}
