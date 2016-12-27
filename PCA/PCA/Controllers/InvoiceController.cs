/* Title: Invoice Contoller
 * Description: Manages invoice information for selected project
 * Location: /Invoice/
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;

namespace PCA.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoice
        public ActionResult Index()
        {
            // Navbar dependencies
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));

            // Creates collection of viewmodel
            List<InvoiceViewModel> viewModel = new List<InvoiceViewModel>();

            // Declarations
            int currentProjectNumber = ViewBag.CurrentProjectNumber;

            // Pulls information from database
            List<Phase> phases = new List<Phase>(db.Phases);
            var invoices = from invoice in db.Invoices
                           join c in db.Contractors on invoice.ContractorId equals c.ContractorId
                           where invoice.ProjectId.Equals(currentProjectNumber)
                           select new
                           {
                                invoice.InvoiceId, invoice.ContractorId, c.Name, invoice.TotalAmount, invoice.DateOfInvoice
                           };


            foreach (var invoice in invoices)
            {
                string datestring = invoice.DateOfInvoice.ToString("MM/dd/yyyy");
                viewModel.Add(new InvoiceViewModel(invoice.InvoiceId, invoice.ContractorId, invoice.Name, invoice.TotalAmount, datestring));
            }


            return View(viewModel);
        }

        public ActionResult Create()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            HttpPostedFileBase file = Request.Files["Document"];


            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }
    }


}