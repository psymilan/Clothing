using System.Web.Mvc;
using Clothing.Web.Data;
using System.Linq;

namespace Clothing.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IClothingRepository repository;
        public CheckoutController(IClothingRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Checkout/
        public ActionResult Index()
        {
            var userId = int.Parse(User.Identity.Name);
            var items = repository.ItemInOrders.Where(i => i.CustomerId == userId && i.OrderId == null).ToList();
            return View(items);
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