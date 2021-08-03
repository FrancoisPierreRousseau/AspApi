using System.ComponentModel.DataAnnotations;


namespace WebApi.Ressources
{
    public class SaveCategoryResource : AbstractSaveRessource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
