using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using PCA.ViewModels;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PCA.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoice
        public ActionResult Index()
        {
            // Sessions
            int currentProjectNum = Convert.ToInt32(Session["DailyReportProject"]);
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.currentProject = currentProjectNum;

            var invoices = from invoice in db.Invoices
                           join cont in db.Contractors on invoice.ContractorId equals cont.ContractorId
                           where invoice.ProjectId == currentProjectNum
                           select new { invoice.InvoiceId, cont.ContractorId, cont.Name, invoice.TotalAmount, invoice.DateOfInvoice, invoice.Status };

            List<InvoiceIndexViewModel> invoiceView = new List<InvoiceIndexViewModel>();

            foreach (var invoice in invoices)
            {
                InvoiceIndexViewModel vm = new InvoiceIndexViewModel();
                vm.InvoiceId = invoice.InvoiceId;
                vm.ContractorId = invoice.ContractorId;
                vm.ContractorName = invoice.Name;
                vm.InvoiceTotal = invoice.TotalAmount;
                vm.InvoiceDate = invoice.DateOfInvoice.ToString("yyyy-MM-dd");
                vm.InvoiceStatus = invoice.Status;
                invoiceView.Add(vm);
            }


            return View(invoiceView);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,ProjectId,ContractorId,AIANumber,InvoiceNumber,AccountNumber,OrderNumber,TotalAmount,TermInDays,DateReceived,DateOfInvoice,Status")] Invoice invoice, HttpPostedFileBase upload)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var invoiceUpload = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.InvoiceImage,
                        ContentType = upload.ContentType
                    };

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        invoiceUpload.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    invoice.Files = new List<File> { invoiceUpload };

                }
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", invoice.ContractorId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", invoice.ProjectId);
            return View(invoice);
        }

        public ActionResult ViewAttachment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Include(s => s.Files).SingleOrDefault(s => s.InvoiceId == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Include(s => s.Files).SingleOrDefault(s => s.InvoiceId == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Include(s => s.Files).SingleOrDefault(s => s.InvoiceId == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", invoice.ContractorId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", invoice.ProjectId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,ProjectId,ContractorId,AIANumber,InvoiceNumber,AccountNumber,OrderNumber,TotalAmount,TermInDays,DateReceived,DateOfInvoice,Status")] Invoice invoice, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", invoice.ContractorId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", invoice.ProjectId);
            return View(invoice);
        }*/

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase upload)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newinvoice = db.Invoices.Find(id);

            //Declars
            ViewBag.ContractorId = new SelectList(db.Contractors, "ContractorId", "Name", newinvoice.ContractorId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "Name", newinvoice.ProjectId);

            if (TryUpdateModel(newinvoice, "",
                new string[] { "InvoiceId", "ProjectId", "ContractorId", "AIANumber", "InvoiceNumber", "AccountNumber", "OrderNumber", "TotalAmount", "TermInDays", "DateReceived", "DateOfInvoice", "Status" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (newinvoice.Files.Any(f => f.FileType == FileType.InvoiceImage))
                        {
                            db.Files.Remove(newinvoice.Files.First(f => f.FileType == FileType.InvoiceImage));
                        }
                        var invoiceUpload = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.InvoiceImage,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            invoiceUpload.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        newinvoice.Files = new List<File> { invoiceUpload };
                    }
                    db.Entry(newinvoice).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(newinvoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            //Get current project
            var systemController = DependencyResolver.Current.GetService<SystemController>();
            systemController.Get();
            var currentList = systemController.Get();
            ViewBag.CurrentProjectString = currentList.ElementAt(0);
            ViewBag.CurrentProjectNumber = int.Parse(currentList.ElementAt(1));
            //---------------------------

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoiceFile = from file in db.Files
                                    where file.InvoiceId == id
                                    select file;

            foreach (var file in invoiceFile)
            {
                db.Files.Remove(file);

            }
            
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*public ActionResult Create()
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
        }*/

        // GET: Invoice/Workflow/5
        public ActionResult Workflow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Include(s => s.Files).SingleOrDefault(s => s.InvoiceId == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Details/5
        public ActionResult WorkflowForward(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Include(s => s.Files).SingleOrDefault(s => s.InvoiceId == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Details/5
        public ActionResult WorkflowReturn(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Include(s => s.Files).SingleOrDefault(s => s.InvoiceId == id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }


    }


}