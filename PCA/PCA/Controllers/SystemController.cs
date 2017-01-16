/* 
 * Name: System Controller
 * Description: Collection of commonly used system functions
*/
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
        // Database context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public void IsLoggedIn()
        {
 
        }

        public void Set(int id)
        {
            HttpContext context = System.Web.HttpContext.Current;
            context.Session["Project"] = id;
        }

        // Sets the project session variable to the selected project Id
        public List<string> Get()
        {
            // Session variable context
            HttpContext context = System.Web.HttpContext.Current;

            string currentUserId = context.Session["UserId"].ToString();
            string currentUserName = context.Session["Username"].ToString();
            List<string> currentList = new List<string>();
            currentList.Add(currentUserId);
            currentList.Add(currentUserName);

            return currentList;
        }

        public bool Auth(List<int> positions, int currentUser)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();

            // Auth

            bool canAccess = false;
             
            var currentPosition = from assignment in db.Assignments
                                  where assignment.AccountId == currentUser
                                  select assignment;


            foreach (var pos in currentPosition)
            {
                if (positions.Contains(pos.PositionId))
                {
                    canAccess = true;
                }
            }

            return canAccess;
        }

    }
}