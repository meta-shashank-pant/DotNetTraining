using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCAssignment;

namespace MVCAssignment.Controllers
{
    [Authorize]
    public class AppUsersController : Controller
    {
        private UserRolesEntities db = new UserRolesEntities();

        // GET: AppUsers
        /// <summary>
        /// Displays the list of users registered.
        /// Every method with Admin role can be accessed by admin only.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.AppUser.ToList());
        }        

        // GET: AppUsers/Details/5
        /// <summary>
        /// Retreive user information with id from the database.
        /// </summary>
        /// <param name="id">Id is integer specifying the primary key.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUser.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        /// <summary>
        /// Create or Register a new user after log in, this can be done by admin only.
        /// </summary>
        /// <param name="appUser">AppUser class object holding the details of the employee entered.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Age,DateOfBirth,Country,EmailId,Password")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.AppUser.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        /// <summary>
        /// Update the user information with given id from database.
        /// </summary>
        /// <param name="id">Id is integer specifying the primary key.</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUser.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Edit([Bind(Include = "Id,Name,Age,DateOfBirth,Country,EmailId,Password")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Details/"+ appUser.Id);
                }
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        /// <summary>
        /// Delete the user information with given id from database.
        /// </summary>
        /// <param name="id">Id is integer specifying the primary key.</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUser.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            AppUser appUser = db.AppUser.Find(id);
            db.AppUser.Remove(appUser);
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
