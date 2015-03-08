using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clothing.Web.DataModels;
using Clothing.Web.Data;
using Clothing.Web.DTOs;
using Clothing.Web.Helpers;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Clothing.Web.Areas.Admin.Controllers
{

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
            var products = repository.Products.ToList();
            var prodList = products.Select(p => new ProductDto
           {
               Id = p.Id,
               Name = p.Name,
               Price = p.Price,
               QuantityAvailable = p.QuantityAvailable,
               Color = p.Color,
               Instructions = p.Instructions,
               Material = p.Material,
               Size =p.Size,
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

        // POST: /Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Products.Add(product);
                repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
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
            repository.Products.Remove(product);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FileUpload(HttpPostedFileBase file, int productId, int imageCategory)
        {
            var productImage = new ProductImage();
            productImage.CreateImage(productId, blob.GetUrl(), imageCategory);
            CloudBlobContainer blobContainer = blob.GetCloudBlobContainer();
            CloudBlockBlob b = blobContainer.GetBlockBlobReference(productImage.ImageName);
            b.UploadFromStream(file.InputStream);

            repository.ProductImages.Add(productImage);
            repository.SaveChanges();

            TempData["notice"] = "Image uploaded";
            return RedirectToAction("Edit", new { id = productId });
        }

        public ActionResult DeleteImage(int id)
        {
            var imageToDelete = repository.ProductImages.Find(id);

            if (System.IO.File.Exists(imageToDelete.FullPath + imageToDelete.ImageName))
                System.IO.File.Delete(imageToDelete.FullPath + imageToDelete.ImageName);

            repository.ProductImages.Remove(imageToDelete);
            repository.SaveChanges();
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
