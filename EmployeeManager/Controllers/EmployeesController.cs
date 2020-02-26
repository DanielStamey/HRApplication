using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,EmailAddress,PreferredContactPhoneNumber,StartDate,EndDate,DepartmentId,PositionId,EmploymentStatusId,ShiftId,ManagerId,FavoriteColor")] Employee employee, HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            employee.TeamMemberPhoto = bytes;

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
            SmtpService.SendNotification("User updated");
            populateEmployeeOptionsViewBag(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,EmailAddress,PreferredContactPhoneNumber,StartDate,EndDate,DepartmentId,PositionId,EmploymentStatusId,ShiftId,ManagerId,FavoriteColor")] Employee employee, HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            if (employee.TeamMemberPhoto != bytes)
            {
                employee.TeamMemberPhoto = bytes;
            }

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
            foreach (Employee emp in managerOf)
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

        private void populateSortFilterSearchViewbags(EmployeesPage employeesPage)
        {
            string sortColumn = employeesPage.SortColumn;
            ViewBag.SearchString = employeesPage.SearchString;
            ViewBag.Sort = string.IsNullOrEmpty(employeesPage.SortColumn) ? "FirstName" : employeesPage.SortColumn;
        }

        private void populateEmployeeOptionsViewBag(int? currentUserId = null)
        {
            ViewBag.Positions = db.Positions.ToList();
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Status = db.EmploymentStatuss.ToList();
            ViewBag.Shifts = db.Shifts.ToList();
            ViewBag.Managers = db.Employees.ToList().Where(e => e.Id != currentUserId).Select(e => new { e.Id, Name = e.FullName });
            //ViewBag.Permissions = db.Permissions.Join(db.EmployeePermissions, p => p.Id, ep => ep.PermissionId, (p, ep) => new { p, ep })
            //    .Join(db.Employees, ep => ep.ep.EmployeeId, e => e.Id, (ep, e) => new { ep, e })
            //    .Select(p => new { p.ep.p.PermissionName, p.e.FirstName })
            //    .ToList();
        }
    }
}
