
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Clothing.Web.Data;
using System.Linq;

namespace Clothing.Web.Controllers
{
    public class HomeController : Controller
    {
        public int UserId
        {
            get { return int.Parse(User.Identity.Name); }
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

        public ActionResult SendEmail(ContactForm contactForm)
        {
            var mail = new MailMessage();
            mail.To.Add("shirin.vakhidova@gmail.com");
            mail.From = new MailAddress(contactForm.Email, contactForm.Name, System.Text.Encoding.UTF8);
            mail.Subject = contactForm.Name + " - " + contactForm.Email + " wrote you on SHIRIN-VAHIDI.COM";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            var body = "Email: " + contactForm.Email + "<br />" + contactForm.Message;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            mail.ReplyTo = new MailAddress(contactForm.Email);
            var client = new SmtpClient
            {
                Credentials = new NetworkCredential("psymilan99@gmail.com", "milan1989"),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            client.Send(mail);

            TempData["email"] = "Message successfully sent. We will reply shortly!";

            return RedirectToAction("Contact");

        }
    }

    public class ContactForm
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}