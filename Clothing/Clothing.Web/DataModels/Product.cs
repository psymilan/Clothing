using System.Collections.Generic;
using System.ComponentModel;

namespace Clothing.Web.DataModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Size { get; set; }
        public string Material { get; set; }
        public string Instructions { get; set; }
        public string Color { get; set; }
        [DisplayName("Quantity available")]
        public int QuantityAvailable { get; set; }

        public int? Sorting { get; set; }

        public virtual ICollection<ItemInOrder> ItemInOrders { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}