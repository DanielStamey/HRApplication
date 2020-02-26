using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    public class AuditLogController : Controller
    {
        DataContext db = new DataContext();

        // GET: AuditLog
        public ActionResult Index()
        {
            return View(db.AuditLogs.ToList());
        }
        
        // GET: AuditLog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Id = id;
            List<AuditLog> EmployeeAudit = db.AuditLogs.Where(a => a.PrimaryKeyValue == id.ToString()).ToList();
            return View(EmployeeAudit);
        }

    }
}