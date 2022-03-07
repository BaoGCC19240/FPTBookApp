using FPTBookApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FPTBookApp2.Controllers
{
    public class HomeController : Controller
    {
    
            private FPTdbEntities db = new FPTdbEntities();
            public ActionResult Index()
            {

                return View(db.products);
            }


        [ChildActionOnly]
        public ActionResult Nav()
        {
            return PartialView("_Nav", db.categories);
        }



        public ActionResult Sortbycat(string id)
        {
            var res = db.products.Where(x => x.CatID == id);
            return View(res.ToList());
        }

        public ActionResult Search(string searchString)
        {
            if (searchString == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var res = db.products.Where(x => x.ProName.Contains(searchString));
                return View(res.ToList());
            }

        }

        public ActionResult AddtoCart(string id)
        {
            var obj = db.products.Find(id);
            return View(obj);
        }

        List<product> lstOD = new List<product>();
        [HttpPost]
        public ActionResult AddtoCart(string id, int qty)
        {
            List<product> checkList = TempData["cart"] as List<product>;
            product pr = db.products.Find(id);
            pr.ProQty = qty;
            if (TempData["cart"] == null)
            {
                lstOD.Add(pr);
                TempData["cart"] = lstOD;
            }
            else
            {
                List<product> lstOD2 = TempData["cart"] as List<product>;
                if(lstOD2.Find(x => x.ProID == id) != null)
                {
                    product ch = lstOD2.Find(x => x.ProID == id);
                    pr.ProQty = ch.ProQty + qty; ;
                    lstOD2.Remove(ch);
                }
                lstOD2.Add(pr);
                TempData["cart"] = lstOD2;
            }
            TempData.Keep();
            return RedirectToAction("Index");
            
        }

        public ActionResult checkout()
        {
            TempData.Keep();


            return View(); 
        }

    }
}