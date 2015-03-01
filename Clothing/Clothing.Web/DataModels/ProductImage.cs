using System.Linq;
using System;

namespace Clothing.Web.DataModels
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string FullPath { get; set; }
        public int ImageCategory { get; set; }
        public Product Product { get; set; }

        public ProductImage CreateImage(int productId, string fullPath, int imageCategory)
        {
            ProductId = productId;
            FullPath = fullPath;
            var guid = Guid.NewGuid();
            var stringGuid = guid.ToString().Take(8).ToArray();
            ImageName = productId + "-" + new string(stringGuid) + ".jpeg";
            ImageCategory = imageCategory;

            return this;
        }
    }

    public static class ImageCategory
    {
        public static int Looks {
            get { return 1; } 
        }
        public static int Catalogue {
            get { return 2; }
        }
    }
}