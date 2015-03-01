using System.Web.Mvc;
using Clothing.Web.Data;
using System.Linq;

namespace Clothing.Web.Controllers
{
    public class HomeController : Controller
    {
        public int UserId { get { return int.Parse(User.Identity.Name); }
        }
        private readonly IClothingRepository repository;
        public HomeController(IClothingRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var itemsInOrder = repository.ItemInOrders.Count(i => i.CustomerId == UserId && i.OrderId == null);
                ViewBag.ItemCount = itemsInOrder;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            if (User.Identity.IsAuthenticated)
            {
                var itemsInOrder = repository.ItemInOrders.Count(i => i.CustomerId == UserId && i.OrderId == null);
                ViewBag.ItemCount = itemsInOrder;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (User.Identity.IsAuthenticated)
            {
                var itemsInOrder = repository.ItemInOrders.Count(i => i.CustomerId == UserId && i.OrderId == null);
                ViewBag.ItemCount = itemsInOrder;
            }
            return View();
        }
    }
}