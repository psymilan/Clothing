using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothing.Web.DataModels
{
    public class ItemInOrder
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}