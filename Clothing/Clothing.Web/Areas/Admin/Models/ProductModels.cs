﻿using System.ComponentModel;

namespace Clothing.Web.Areas.Admin.Models
{
    public class ProductListModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [DisplayName("Quantity available")]
        public int QuantityAvailable { get; set; }

        [DisplayName("Has images")]
        public bool HasImages { get; set; }
        public string[] ImagePaths { get; set; }
    }
}
