using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManager;

namespace EmployeeManager.Controllers
{
    public class EmployeesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Employees
        public ActionResult Index(string sortOrder, string searchString)
        {
            var employees = db.Employees.Select(e => e);
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.LastName.Contains(searchString)
                                       || e.FirstName.Contains(searchString));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
                case "Date":
                    employees = employees.OrderBy(e => e.StartDate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(e => e.StartDate);
                    break;
                default:
                    employees = employees.OrderBy(e => e.LastName);
                    break;
            }

            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Employees/Create
        public ActionResult Create()
        {
            populateViewBag();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,EmailAddress,PreferredContactPhoneNumber,StartDate,EndDate,DepartmentId,PositionId,EmploymentStatusId,ShiftId,ManagerId,FavoriteColor")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            populateViewBag();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            populateViewBag(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,EmailAddress,PreferredContactPhoneNumber,StartDate,EndDate,DepartmentId,PositionId,EmploymentStatusId,ShiftId,ManagerId,FavoriteColor")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            populateViewBag(employee.Id);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Employee> managerOf = db.Employees.Where(e => e.ManagerId == id).ToList();
            foreach(Employee emp in managerOf)
            {
                db.Employees.Find(emp.Id).ManagerId = null;
            }
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        private void populateViewBag(int? currentUserId = null)
        {
            ViewBag.Positions = db.Positions.ToList();
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Status = db.EmploymentStatuss.ToList();
            ViewBag.Shifts = db.Shifts.ToList();
            ViewBag.Managers = db.Employees.ToList().Where(e => e.Id != currentUserId).Select(e => new { e.Id, Name = e.FullName });
        }
    }
}
