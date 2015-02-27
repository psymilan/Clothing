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
            var images = repository.ProductImages.Where(i => i.ImageCategory == ImageCategory.Catalogue).Select(i => i.ImageName).ToArray();
            var products = repository.Products.ToList().Select(p => new ProductDto
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityAvailable = p.QuantityAvailable,
                ShortDescription = p.ShortDescription,
                ImagePaths = images.Where(i => i.StartsWith(p.Id.ToString(CultureInfo.InvariantCulture)))
            }).ToList();

            return View(products);
        }

        public int AddItem(int id)
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

            var count = repository.ItemInOrders.Count(i => i.CustomerId == userId && i.OrderId == null);
            return count;
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