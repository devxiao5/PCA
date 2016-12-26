using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCA.Controllers
{
    public class ContractController : Controller
    {
        // GET: Contract
        public ActionResult Index()
        {
            // Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            // -----------

            return View();
        }

        public ActionResult GeneralCondition()
        {
            return View();

        }

        public ActionResult MaterialPurchaseOrder()
        {
            return View();

        }
    }
}