
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ProductToReturnDTO
    {
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [Url]
        public string PictureUrl { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductBrandName { get; set; }
    }
}