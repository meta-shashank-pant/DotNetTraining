using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApiAssignment.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeDBEntities db = new EmployeeDBEntities();
        
        /// <summary>
        /// Index page displaying the list of employees.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        /// <summary>
        /// Create page for creating the new employee record.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();            
        }

        /// <summary>
        /// Update page for updating the employee record.
        /// </summary>
        /// <param name="id">Input id of the employee.</param>
        /// <returns></returns>
        public ActionResult Update(int? id)
        {
            ViewBag.Title = "Update Details";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// Details page displaying the detail of the employee.
        /// </summary>
        /// <param name="id">Input id of employee.</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "Details";
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// Delete page for deleting employee record.
        /// </summary>
        /// <param name="id">Input id of employee</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// Disposing the database entity object.
        /// </summary>
        /// <param name="disposing"></param>
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
