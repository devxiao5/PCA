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

        public bool Auth(List<int> positions, int currentUser)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            int currentProjectNumber = ViewBag.CurrentProjectNumber;

            // Auth

            bool canAccess = false;
             
            var currentPosition = from assignment in db.Assignments
                                  where assignment.AccountId == currentUser &&
                                        assignment.ProjectId == currentProjectNumber
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