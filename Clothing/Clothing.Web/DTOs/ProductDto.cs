using System.Collections.Generic;
using System.ComponentModel;

namespace Clothing.Web.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public decimal Price { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public string Instructions { get; set; }
        [DisplayName("Quantity available")]
        public int QuantityAvailable { get; set; }
        public IEnumerable<string> ImagePaths { get; set; }

        [DisplayName("Has images?")]
        public bool HasImages { get; set; }
        public string Color { get; set; }
    }
}