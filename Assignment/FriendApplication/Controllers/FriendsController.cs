using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FriendApplication;

namespace FriendApplication.Controllers
{
    public class FriendsController : Controller
    {
        private FriendDbEntities db = new FriendDbEntities();

        // GET: Friends
        //These are routing attribute, Mentioned in Route config.
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            return View(db.Friend.ToList());
        }

        // GET: Friends/Details/5
        /// <summary>
        /// Get details of an friend with the given id.
        /// </summary>
        /// <param name="id">Integer id representing the primary key.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friend.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // GET: Friends/Create
        /// <summary>
        /// Create a new friend and store in database.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            using(var context = new FriendDbEntities())
            {
                var Friendid = (from f in context.Friend
                          select f.Id).Max();
                try
                {
                    Friendid = Convert.ToInt32(Friendid);
                    ViewBag.FriendId = Friendid + 1;
                }
                catch
                {
                    ViewBag.FriendId = 1;
                }
                return View();
            }
            
        }

        // POST: Friends/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]     
        public ActionResult Create([Bind(Include = "Id,Name,Place")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Friend.Add(friend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(friend);
        }

        // GET: Friends/Edit/5
        /// <summary>
        /// Edit the friend with the given id.
        /// </summary>
        /// <param name="id">Integer id representing the primary key.</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friend.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Place")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(friend);
        }

        // GET: Friends/Delete/5
        /// <summary>
        /// Delete the friend entry from the database.
        /// </summary>
        /// <param name="id">Integer id representing the primary key.</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friend friend = db.Friend.Find(id);
            if (friend == null)
            {
                return HttpNotFound();
            }
            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Friend friend = db.Friend.Find(id);
            db.Friend.Remove(friend);
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
