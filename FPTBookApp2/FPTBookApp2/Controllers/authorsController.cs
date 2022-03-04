using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FPTBookApp2.Models;

namespace FPTBookApp2.Controllers
{
    public class authorsController : Controller
    {
        private FPTdbEntities db = new FPTdbEntities();
        public ActionResult Index()
        {
            return View(db.authors.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(author author)
        {
            if (author.ImageFile != null)
            {
                string path = Path.Combine(Server.MapPath("~/Image/"), Path.GetFileName(author.ImageFile.FileName));
                author.ImageFile.SaveAs(path);
                author.auImage = Path.GetFileName(author.ImageFile.FileName);
            }
            db.authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(author author)
        {
            author old = db.authors.Find(author.auID);
            if (author.ImageFile != null)
            {
                string path = Path.Combine(Server.MapPath("~/Image/"), Path.GetFileName(author.ImageFile.FileName));
                author.ImageFile.SaveAs(path);
                old.auImage = Path.GetFileName(author.ImageFile.FileName);
            }
            else
            {

                old.auImage = author.auImage;
            }
            db.Entry(old).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(string id)
        {
                author obj = db.authors.Find(id);
                db.authors.Remove(obj);
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
