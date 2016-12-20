using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCA.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.IsLoggedIn();
            return View();
        }
    }
}