using System.Collections.Generic;
using System.ComponentModel;

namespace Clothing.Web.DataModels
{
    public class Product
    {
        public string Name { get; set; }
        public int Id { get; set; }

        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [DisplayName("Quantity available")]
        public int QuantityAvailable { get; set; }

        public virtual ICollection<ItemInOrder> ItemInOrders { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}