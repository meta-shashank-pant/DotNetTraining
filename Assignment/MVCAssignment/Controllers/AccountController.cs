using MVCAssignment.Models;
using MVCAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCAssignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            using(var context = new UserRolesEntities())
            {
                bool isValid = context.AppUser.Any(x => x.EmailId == model.EmailId && x.Password == model.Password);                
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.EmailId, false);
                    if(User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "AppUsers");
                    }
                    else
                    {
                        int id = context.AppUser.Where(x => x.EmailId == model.EmailId).Select(x => x.Id).First();
                        return RedirectToAction("Details/" + id, "AppUsers");
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email Id or Password.");
                    return View();
                }
            }          
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(AppUser model)
        {
            using(var context = new UserRolesEntities())
            {
                context.AppUser.Add(model);
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