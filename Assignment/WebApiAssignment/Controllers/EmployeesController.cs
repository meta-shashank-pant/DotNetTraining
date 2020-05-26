using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiAssignment;

namespace WebApiAssignment.Controllers
{
    public class EmployeesController : ApiController
    {
        private EmployeeDBEntities db = new EmployeeDBEntities();

        // GET: api/Employees
        /// <summary>
        /// Get method for getting all employees from database
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/Employees/5
        /// <summary>
        /// Get method to get employee with the given id.
        /// </summary>
        /// <param name="id">Input id of employee.</param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Get method for getting employees by gender.
        /// Here, Attribute routing is used.
        /// </summary>
        /// <param name="id">Input id of employee.</param>
        /// <returns></returns>
        [Route("api/Employees/{id}/gender")]
        public IQueryable<Employee> GetByGender(string id)
        {
            if(id == "all")
            {
                return db.Employees;
            }
            return db.Employees.Where(e => e.Gender == id);
        }        

        // PUT: api/Employees/5
        /// <summary>
        /// Put method for updating the record in the database.
        /// </summary>
        /// <param name="id">Input id of employee</param>
        /// <param name="employee">Employee object storing the value of employee.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.ID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        /// <summary>
        /// Post method to create new employee.
        /// </summary>
        /// <param name="employee">Employee object with required data.</param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.ID }, employee);
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Delete method for removing the employee record from database.
        /// </summary>
        /// <param name="id">Input id of employee</param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        /// <summary>
        /// Disposing the object of database entity.
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

        /// <summary>
        /// Checks wheather employee with the given id exists or not.
        /// </summary>
        /// <param name="id">Input id of employee</param>
        /// <returns></returns>
        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.ID == id) > 0;
        }
    }
}