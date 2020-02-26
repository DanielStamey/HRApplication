using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    public class EmploymentStatusController : Controller
    {
        private DataContext db = new DataContext();

        // GET: EmploymentStatus
        public ActionResult Index()
        {
            return View(db.EmploymentStatuss.ToList());
        }

        // GET: EmploymentStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentStatus employmentStatus = db.EmploymentStatuss.Find(id);
            if (employmentStatus == null)
            {
                return HttpNotFound();
            }
            return View(employmentStatus);
        }

        // GET: EmploymentStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmploymentStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] EmploymentStatus employmentStatus)
        {
            if (ModelState.IsValid)
            {
                db.EmploymentStatuss.Add(employmentStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employmentStatus);
        }

        // GET: EmploymentStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentStatus employmentStatus = db.EmploymentStatuss.Find(id);
            if (employmentStatus == null)
            {
                return HttpNotFound();
            }
            return View(employmentStatus);
        }

        // POST: EmploymentStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] EmploymentStatus employmentStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employmentStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employmentStatus);
        }

        // GET: EmploymentStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentStatus employmentStatus = db.EmploymentStatuss.Find(id);
            if (employmentStatus == null)
            {
                return HttpNotFound();
            }
            return View(employmentStatus);
        }

        // POST: EmploymentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmploymentStatus employmentStatus = db.EmploymentStatuss.Find(id);
            db.EmploymentStatuss.Remove(employmentStatus);
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
