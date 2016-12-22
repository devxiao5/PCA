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
            int projectsession = (int)(context.Session["Project"]);

            // Query database for selected project
            var currentProject = from project in db.Projects
                                     where project.ProjectId == projectsession
                                     select project;

            // Set project values to pass to controller
            foreach (var item in currentProject)
            {
                string currentUserId = context.Session["UserId"].ToString();
                string currentUserName = context.Session["Username"].ToString();
                string currentProjectNumber = item.ProjectId.ToString();
                string currentProjectName = item.Name;
                List<string> currentList = new List<string>();
                currentList.Add("[" + currentProjectNumber + "]" + " - " + currentProjectName);
                currentList.Add(currentProjectNumber);
                currentList.Add(currentUserId);
                currentList.Add(currentUserName);
                return currentList;
            }

            return new List<string>();
        }
    }
}