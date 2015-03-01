using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Clothing.Web.Data;
using Clothing.Web.DataModels;
using Clothing.Web.DTOs;

namespace Clothing.Web.Controllers
{
    public class LooksController : Controller
    {
        private readonly IClothingRepository repository;
        public LooksController(IClothingRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Looks/
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