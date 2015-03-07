using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Clothing.Web.Data;
using Clothing.Web.DataModels;
using Clothing.Web.DTOs;

namespace Clothing.Web.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IClothingRepository repository;
        public CatalogueController(IClothingRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Catalogue/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Identity.Name);
                var itemsInOrder = repository.ItemInOrders.Count(i => i.CustomerId == userId && i.OrderId == null);
                ViewBag.ItemCount = itemsInOrder;
            }
            var images = repository.ProductImages.Where(i => i.ImageCategory == ImageCategory.Looks).Select(i => i.ImageName).ToArray();
            var products = repository.Products.ToList().Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityAvailable = p.QuantityAvailable,
                ImagePaths = images.Where(i => i.StartsWith(p.Id.ToString(CultureInfo.InvariantCulture)))
            }).ToList();

            return View(products);
        }

        public int AddItem(int id)
        {
            int count = 0;
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Identity.Name);

                var product = repository.Products.SingleOrDefault(p => p.Id == id);
                var itemExist = repository.ItemInOrders.SingleOrDefault(i => i.CustomerId == userId && i.ProductId == id);
                if (itemExist != null)
                {
                    itemExist.Quantity++;
                }
                else
                {
                    var item = new ItemInOrder
                    {
                        CustomerId = userId,
                        Price = product.Price,
                        ProductId = id,
                        Quantity = 1,
                        ImagePath = "asDasd"

                    };
                    repository.ItemInOrders.Add(item);
                }
                repository.SaveChanges();

                count = repository.ItemInOrders.Where(i => i.CustomerId == userId && i.OrderId == null).Sum(o => o.Quantity);
            }
            return count;
        }

        public ActionResult Detail(int id)
        {
            var product = repository.Products.SingleOrDefault(p => p.Id == id);

            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Identity.Name);
                var itemsInOrder = repository.ItemInOrders.Count(i => i.CustomerId == userId && i.OrderId == null);
                ViewBag.ItemCount = itemsInOrder;
            }
            var images = repository.ProductImages.Where(i => i.ImageCategory == ImageCategory.Catalogue).Select(i => i.ImageName).ToArray();
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityAvailable = product.QuantityAvailable,
                ImagePaths = images.Where(i => i.StartsWith(product.Id.ToString(CultureInfo.InvariantCulture)))
            };
            return View(productDto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}