using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clothing.Web.Data;

namespace Clothing.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClothingRepository repository;
        public HomeController(IClothingRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}