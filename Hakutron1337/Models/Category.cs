using System.ComponentModel.DataAnnotations;

namespace Hakutron1337.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Relación: 1 categoría → muchos productos
        public List<Product>? Products { get; set; }
    }
}
