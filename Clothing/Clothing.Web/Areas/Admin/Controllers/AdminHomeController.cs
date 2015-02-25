using System.IO;
using System.Web;
using System.Web.Mvc;
using Clothing.Web.Data;
using Clothing.Web.DataModels;

namespace Clothing.Web.Areas.Admin.Controllers
{
    [Authorize(Users = "Milance")]
    public class AdminHomeController : Controller
    {

        private readonly IClothingRepository repository;
        public AdminHomeController(IClothingRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Admin/AdminHome/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase file)
        {
            repository.Products.Add(product);
            repository.SaveChanges();
            TempData["notice"] = "New product added";


            return RedirectToAction("Index");
        }

    }
}