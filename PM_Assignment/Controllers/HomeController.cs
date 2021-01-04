using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PM_Assignment.Models;
using System.Web.Security;

namespace PM_Assignment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user_master model)
        {
            using (var context = new ProductManagementEntities())
            {
                bool isValid = context.user_master.Any(x => x.EmailID == model.EmailID && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.EmailID, false);
                    return RedirectToAction("Index", "DataConnect");
                }

                ModelState.AddModelError("", "Invalid emailid and password");
                return View();
            }
                
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(user_master model)
        {
            using (var context = new ProductManagementEntities())
            {
                context.user_master.Add(model);
                context.SaveChanges();  
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}