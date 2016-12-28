using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCA.Models;

namespace PCA.Controllers
{
    public class FileSignatureController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileSignature
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.FileSignatures.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}