using System.ComponentModel.DataAnnotations;

namespace Hakutron1337.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        // Clave foránea
        public int CategoryId { get; set; }

        // Navegación
        public Category Category { get; set; }

    }
}
