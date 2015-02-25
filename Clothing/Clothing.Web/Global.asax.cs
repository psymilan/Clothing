using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Clothing.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

        //    if (authCookie != null)
        //    {
        //        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        //        var serializer = new JavaScriptSerializer();

        //        var serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

        //        var newUser = new CustomPrincipal(authTicket.Name)
        //        {
        //            Id = serializeModel.Id,
        //            FirstName = serializeModel.FirstName,
        //            LastName = serializeModel.LastName
        //        };

        //        HttpContext.Current.User = newUser;
        //    }
        //}
    }
}
