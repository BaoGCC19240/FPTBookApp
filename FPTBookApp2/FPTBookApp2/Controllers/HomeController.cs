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



    }
}