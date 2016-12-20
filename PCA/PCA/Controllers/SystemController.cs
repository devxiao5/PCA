using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;

namespace PCA.Controllers
{
    public class SystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public void IsLoggedIn()
        {
 
        }
    }
}