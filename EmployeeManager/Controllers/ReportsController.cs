using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Attrition()
        {
            Report report = new Report();
            return View(report);
        }

        public ActionResult EmployeeCounts()
        {
            Report report = new Report();
            return View(report);
        }
    }
}