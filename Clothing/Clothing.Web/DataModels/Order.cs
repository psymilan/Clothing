using System;
using System.Collections.Generic;

namespace Clothing.Web.DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public DateTime DateCreated { get;set; }
        public decimal TotalPrice { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<ItemInOrder> ItemInOrders { get; set; }
    }
}