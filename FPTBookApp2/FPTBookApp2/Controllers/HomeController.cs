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

        public ActionResult AddtoCart(string id)
        {
            var obj = db.products.Find(id);
            return View(obj);
        }

        List<OrderDetail> lstOD = new List<OrderDetail>();
        [HttpPost]
        public ActionResult AddtoCart(string id, string qty)
        {
            var p = db.products.Find(id);
            OrderDetail ord = new OrderDetail();

            ord.ProID = p.ProID;
            if(qty != null){
                ord.qty = Convert.ToInt32(qty);
            }
            else
            {
                ord.qty = 1;
            }
            ord.price = p.ProPrice;

            lstOD.Add(ord);

            TempData["cart"] = lstOD;
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