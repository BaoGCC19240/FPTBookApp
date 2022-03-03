using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Data;
using System.Web.Mvc;
using FPTBookApp2.Models;



namespace FPTBookApp2.Controllers
{
    public class productsController : Controller
    {
        private FPTdbEntities db = new FPTdbEntities();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.author).Include(p => p.category);
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.auID = new SelectList(db.authors, "auID", "auName");
            ViewBag.CatID = new SelectList(db.categories, "CatID", "CatName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(product product)
        {
            if(product.myfile != null)
            {
                string path = Path.Combine(Server.MapPath("~/Image/"), Path.GetFileName(product.myfile.FileName));
                product.myfile.SaveAs(path);
                product.ProImage =Path.GetFileName(product.myfile.FileName);
            }


            db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            
        }

        // GET: products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.auID = new SelectList(db.authors, "auID", "auName", product.auID);
            ViewBag.CatID = new SelectList(db.categories, "CatID", "CatName", product.CatID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(product product)
        {
            product PDmodify = db.products.Find(product.ProID);
            if (product.myfile != null)
            {
                string path = Path.Combine(Server.MapPath("~/Image/"), Path.GetFileName(product.myfile.FileName));
                product.myfile.SaveAs(path);
                PDmodify.ProImage = Path.GetFileName(product.myfile.FileName);
            }
                db.Entry(PDmodify).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
            ViewBag.auID = new SelectList(db.authors, "auID", "auName", product.auID);
            ViewBag.CatID = new SelectList(db.categories, "CatID", "CatName", product.CatID);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
