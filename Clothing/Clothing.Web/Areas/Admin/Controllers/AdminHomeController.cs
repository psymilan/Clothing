using System.Web.Mvc;
using Clothing.Web.Data;

namespace Clothing.Web.Areas.Admin.Controllers
{

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

    }
}