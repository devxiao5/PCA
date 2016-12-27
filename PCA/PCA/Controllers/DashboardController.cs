﻿/* 
 * Name: Dashboard Controller
 * Description: Controlls Project homepages and project selection
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;

namespace PCA.Controllers
{
    public class DashboardController : Controller
    {
        // Database context
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard/Index/1
        public ActionResult Index(int id)
        {
            // Connects to System Controller
            var systemController = DependencyResolver.Current.GetService<SystemController>();

            // Session variable context
            HttpContext context = System.Web.HttpContext.Current;

            // Sets commonly used sessions variables and viewbag objects
            systemController.Set(id);
            var currentList = systemController.Get();
            @ViewBag.CurrentProjectString = currentList.ElementAt(0);
            @ViewBag.CurrentProjectNumber = currentList.ElementAt(1);
            @ViewBag.CurrentUserId = currentList.ElementAt(2);
            @ViewBag.CurrentUserName = currentList.ElementAt(3);

            return View();
        }


        // GET: Dashboard/Select
        public ActionResult Select()
        {
            // Get all current projects
            var projects = db.Projects.ToList();

            return View(projects);
        }
    }
}