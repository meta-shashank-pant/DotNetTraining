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
    /// <summary>
    /// Only use of this controller is to view, create, reset and delete the user role(Admin and User).
    /// Only Admin can access this controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private UserRolesEntities db = new UserRolesEntities();

        // GET: UserRoles
        public ActionResult Index()
        {
            var userRole = db.UserRole.Include(u => u.AppUser).Include(u => u.Role);
            return View(userRole.ToList());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRole.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AppUser, "Id", "Name");
            ViewBag.Id = new SelectList(db.Role, "Id", "Role1");
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RoleId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.UserRole.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.AppUser, "Id", "Name", userRole.Id);
            ViewBag.Id = new SelectList(db.Role, "Id", "Role1", userRole.Id);
            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRole.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AppUser, "Id", "Name", userRole.Id);
            ViewBag.Id = new SelectList(db.Role, "Id", "Role1", userRole.Id);
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RoleId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AppUser, "Id", "Name", userRole.Id);
            ViewBag.Id = new SelectList(db.Role, "Id", "Role1", userRole.Id);
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRole.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole userRole = db.UserRole.Find(id);
            db.UserRole.Remove(userRole);
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
