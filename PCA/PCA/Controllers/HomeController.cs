using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;

namespace PCA.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var usr =
                    db.Accounts.Where(m => m.Username == user.Username && m.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserId"] = user.AccountId.ToString();
                    Session["Username"] = user.Username.ToString();
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is incorrect.");
                }
            }

            return View();


        }
    }
}