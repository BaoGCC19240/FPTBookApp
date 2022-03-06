using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using FPTBookApp2.Models;

namespace FPTBookApp2.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Account
        FPTdbEntities db = new FPTdbEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Account acc)
        {
            acc.pass = EncodePassword(acc.pass);
            db.Accounts.Add(acc);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public static string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = System.Text.ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string accid, string pass)
        {
            var ch_pass = EncodePassword(pass);
            var obj = db.Accounts.Where(x => x.AccID.Equals(accid) && x.pass.Equals(ch_pass)).FirstOrDefault();
            if (obj != null)
            {
                Session["fullname"] = obj.fullname;
                Session["state"] = Convert.ToInt32(obj.state);
                return RedirectToAction("Index", "Home");
            }
            Response.Write("<script>alert('Login failed, please try again');</script>");
            return View();

        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Account acc)
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
