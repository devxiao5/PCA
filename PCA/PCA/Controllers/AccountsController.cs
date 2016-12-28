using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCA.Models;
using System.Data.Entity.Infrastructure;

namespace PCA.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Include(s => s.FileSignatures).SingleOrDefault(s => s.AccountId == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,FirstName,LastName,Email,Username,Password,ConfirmPassword,Type,CanLogin")] Account account, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
               if (upload != null && upload.ContentLength > 0)
                {
                    var signature = new FileSignature
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileTypeSignature = FileTypeSignature.Signature,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        signature.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    account.FileSignatures = new List<FileSignature> { signature };
                }
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Include(s => s.FileSignatures).SingleOrDefault(s => s.AccountId == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountUpdate = db.Accounts.Find(id);
            if (TryUpdateModel(accountUpdate, "",
                new string[] { "AccountId", "FirstName", "LastName", "Email", "Username", "Password", "ConfirmPassword", "Type", "CanLogin" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (accountUpdate.FileSignatures.Any(f => f.FileTypeSignature == FileTypeSignature.Signature))
                        {
                            db.FileSignatures.Remove(accountUpdate.FileSignatures.First(f => f.FileTypeSignature == FileTypeSignature.Signature));
                        }
                        var signature = new FileSignature
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileTypeSignature = FileTypeSignature.Signature,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            signature.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        accountUpdate.FileSignatures = new List<FileSignature> { signature };
                    }
                    db.Entry(accountUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(accountUpdate);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
