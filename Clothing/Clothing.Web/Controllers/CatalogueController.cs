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
            var products = repository.Products.OrderBy(p => p.Sorting).ToList().Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                QuantityAvailable = p.QuantityAvailable,
                ImagePaths = p.ProductImages.Select(i => i.FullPath + i.ImageName)
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
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityAvailable = product.QuantityAvailable,
                Size = product.Size, 
                Material = product.Material,
                Instructions = product.Instructions,
                ImagePaths = product.ProductImages.Select(i => i.FullPath + i.ImageName)
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