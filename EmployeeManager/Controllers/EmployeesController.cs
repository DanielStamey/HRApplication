using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
        public ActionResult Index(EmployeesPage employeesPage)
        {
            populateSortFilterSearchViewbags(employeesPage);

            return View(employeesPage);
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
            populateEmployeeOptionsViewBag();
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
            populateEmployeeOptionsViewBag();
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

            populateEmployeeOptionsViewBag(id);
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
            populateEmployeeOptionsViewBag(employee.Id);
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

        private IQueryable<Employee> getSortedFilteredSearchedData(string sortOrder, string searchString, string filterColumn, string filterOperation, string filterString)
        {
            var employees = db.Employees.Select(e => e);

            //if (!string.IsNullOrEmpty(filterColumn) && !string.IsNullOrEmpty(filterOperation) && !string.IsNullOrEmpty(filterString))
            //{
            //    ParameterExpression pe = Expression.Parameter(typeof(Employee), "emp");

            //    Expression left = Expression.Property(pe, typeof(Employee).GetProperty("Department"));
            //    Expression left2 = Expression.Property(left, typeof(Department).GetProperty("DepartmentName"));
            //    Expression left3 = Expression.Call(left2, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            //    Expression right = Expression.Constant(filterString.ToLower());
            //    Expression e = Expression.Equal(left3, right);

            //    MethodCallExpression whereCallExpression = Expression.Call(typeof(Queryable), "Where", new Type[] { employees.ElementType }, employees.Expression,
            //        Expression.Lambda<Func<Employee, bool>>(e, new ParameterExpression[] { pe }));

            //    employees = employees.Provider.CreateQuery<Employee>(whereCallExpression);
            //}

            switch (sortOrder)
            {
                case "first_desc":
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;
                case "last":
                    employees = employees.OrderBy(e => e.LastName);
                    break;
                case "last_desc":
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
                case "position":
                    employees = employees.OrderBy(e => e.Position.PositionName);
                    break;
                case "position_desc":
                    employees = employees.OrderByDescending(e => e.Position.PositionName);
                    break;
                case "department":
                    employees = employees.OrderBy(e => e.Department.DepartmentName);
                    break;
                case "department_desc":
                    employees = employees.OrderByDescending(e => e.Department.DepartmentName);
                    break;
                case "status":
                    employees = employees.OrderBy(e => e.EmploymentStatus.Status);
                    break;
                case "status_desc":
                    employees = employees.OrderByDescending(e => e.EmploymentStatus.Status);
                    break;
                case "shift":
                    employees = employees.OrderBy(e => e.Shift.ShiftName);
                    break;
                case "shift_desc":
                    employees = employees.OrderByDescending(e => e.Shift.ShiftName);
                    break;
                case "manager":
                    employees = employees.OrderBy(e => e.Manager.FirstName);
                    break;
                case "manager_desc":
                    employees = employees.OrderByDescending(e => e.Manager.FirstName);
                    break;
                default:
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
            }

            return employees;
        }

        private void populateSortFilterSearchViewbags(EmployeesPage employeesPage)
        {
            string sortColumn = employeesPage.SortColumn;
            ViewBag.SearchString = employeesPage.SearchString;
            ViewBag.Sort = string.IsNullOrEmpty(employeesPage.SortColumn) ? "FirstName" : employeesPage.SortColumn;
            //ViewBag.FirstName = String.IsNullOrEmpty(sortColumn) ? "first_desc" : "";
            //ViewBag.LastName = sortColumn == "last" ? "last_desc" : "last";
            //ViewBag.Position = sortColumn == "position" ? "position_desc" : "position";
            //ViewBag.Department = sortColumn == "department" ? "department_desc" : "department";
            //ViewBag.Status = sortColumn == "status" ? "status_desc" : "status";
            //ViewBag.Shift = sortColumn == "shift" ? "shift_desc" : "shift";
            //ViewBag.Manager = sortColumn == "manager" ? "manager_desc" : "manager";
        }

        private void populateEmployeeOptionsViewBag(int? currentUserId = null)
        {
            ViewBag.Positions = db.Positions.ToList();
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Status = db.EmploymentStatuss.ToList();
            ViewBag.Shifts = db.Shifts.ToList();
            ViewBag.Managers = db.Employees.ToList().Where(e => e.Id != currentUserId).Select(e => new { e.Id, Name = e.FullName });
        }
    }
}
