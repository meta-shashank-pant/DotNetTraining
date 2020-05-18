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

        /// <summary>
        /// Authenticate the employee against their detail in database.
        /// </summary>
        /// <param name="model">LoginInfo class object storing the credentials.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            using(var context = new UserRolesEntities())
            {
                /// Validating user credentials with the database.
                bool isValid = context.AppUser.Any(x => x.EmailId == model.EmailId && x.Password == model.Password);                
                if (isValid)
                {
                    /// Setting Auth Cookies to keep track of the current account.
                    FormsAuthentication.SetAuthCookie(model.EmailId, false);

                    /// If user is admin, they will be redirected to the index page, else to the details page with the current user id.
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

        /// <summary>
        /// Sign Up or Registration in the database.
        /// </summary>
        /// <param name="model">AppUser class object having the same attribute as database.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(AppUser model)
        {
            using(var context = new UserRolesEntities())
            {
                context.AppUser.Add(model);
                context.SaveChanges();             
                
                /// There are 2 roles: User(1) and Admin(2).
                /// For every registering user, they are given RoleId = 1, by default.
                /// Later admin has power to change the Role Id.
                UserRole userRole = new UserRole
                {                    
                    UserId = model.Id,
                    RoleId = 1
                };
                context.UserRole.Add(userRole);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Logout will return user to login page and sign out from the FormsAuthentication Cookies.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}