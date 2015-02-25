using System.Web.Mvc;
using Clothing.Web.Data;
using Clothing.Web.DataModels;

namespace Clothing.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IClothingRepository repository;
        public BlogController(IClothingRepository repo)
        {
            repository = repo;
        }
        //
        // GET: /Blog/
        public ActionResult Index()
        {
            var blog = new Blog
            {
                Id = 1,
                ImagePath = "asdf",
                Text = "Blog text",
                Title = "Blog title"
            };
            return View(blog);
        }
    }
}