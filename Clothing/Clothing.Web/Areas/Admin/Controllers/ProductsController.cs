using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clothing.Web.DataModels;
using Clothing.Web.Data;
using Clothing.Web.DTOs;
using Clothing.Web.Helpers;

namespace Clothing.Web.Areas.Admin.Controllers
{
    [Authorize(Users = "2")]
    public class ProductsController : Controller
    {
        private BlobStorageHelper blob;
        private readonly IClothingRepository repository;
        public ProductsController(IClothingRepository repo)
        {
            blob = new BlobStorageHelper();
            repository = repo;
        }
        // GET: /Admin/Products/
        public ActionResult Index()
        {
            var products = repository.Products.OrderBy(p => p.Sorting).ToList();
            var prodList = products.Select(p => new ProductDto
           {
               Id = p.Id,
               Name = p.Name,
               Price = p.Price,
               QuantityAvailable = p.QuantityAvailable,
               Color = p.Color,
               Instructions = p.Instructions,
               Material = p.Material,
               Size = p.Size,
               HasImages = p.ProductImages.Any(),
               ImagePaths = p.ProductImages.Select(i => i.FullPath + i.ImageName).ToList()
           }).ToList();
            return View(prodList);
        }

        // GET: /Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.Products.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            repository.Products.Add(product);
            repository.SaveChanges();
            TempData["notice"] = "New product added";
            return RedirectToAction("Index", "Products");
        }

        // GET: /Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            var images = repository.ProductImages.Where(i => i.ProductId == id).ToList();
            ViewBag.Images = images;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.Products.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Entry(product).State = EntityState.Modified;
                repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.Products.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repository.Products.Find(id);

            foreach (var image in product.ProductImages.ToList())
            {
                repository.ProductImages.Remove(image);
                blob.DeleteImage(image);
            }
            repository.Products.Remove(product);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FileUpload(HttpPostedFileBase file, int productId, int imageCategory)
        {
            if (file == null)
            {
                TempData["bad"] = "Choose image!";
                return RedirectToAction("Edit", new { id = productId });
            }
            var productImage = new ProductImage();
            productImage.CreateImage(productId, blob.GetUrl(), imageCategory);
            blob.SaveImage(productImage, file);
            repository.ProductImages.Add(productImage);
            repository.SaveChanges();

            TempData["good"] = "Image uploaded";
            return RedirectToAction("Edit", new { id = productId });
        }

        public ActionResult DeleteImage(int id)
        {
            var imageToDelete = repository.ProductImages.Find(id);
            blob.DeleteImage(imageToDelete);
            repository.ProductImages.Remove(imageToDelete);
            repository.SaveChanges();

            TempData["good"] = "Image deleted!";
            return RedirectToAction("Edit", new { id = imageToDelete.ProductId });
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
