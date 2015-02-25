using System.Collections.Generic;
using System.ComponentModel;
using Clothing.Web.DataModels;

namespace Clothing.Web.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int Id { get; set; }

        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [DisplayName("Quantity available")]
        public int QuantityAvailable { get; set; }
        public IEnumerable<string> ImagePaths { get; set; }
    }
}