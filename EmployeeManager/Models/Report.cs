using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;

namespace EmployeeManager
{
    public class Report
    {
        DataContext db = new DataContext();

        [NotMapped]
        public List<Record> WeeklyHireCount
        {
            get
            {
                return db.Employees.OrderByDescending(e => e.StartDate)
                    .GroupBy(e => new { year = e.StartDate.Year, Week = SqlFunctions.DatePart("week", e.StartDate) } )
                    .Select(e => new Record() { Label = e.Key.year.ToString() + " " + e.Key.Week.ToString(), Count = e.Count() }).ToList();
            }
        }


        [NotMapped]
        public List<Record> YearlyTerminationCount
        {
            get
            {
                return db.Employees.OrderByDescending(e => e.EndDate)
                    .Where(e => e.EndDate != null)
                    .GroupBy(e => new { e.EndDate.Value.Year })
                    .Select(e => new Record(){ Label = e.Key.Year.ToString(), Count = e.Count() } ).ToList();
            }
        }

        [NotMapped]
        public List<Record> ManagerEmployeeCount
        {
            get
            {
                return db.Employees.Where(e => e.ManagerId != null)
                    .OrderBy(e => e.ManagerId)
                    .GroupBy(e => e.Manager )
                    .Select(e => new Record() { Label = e.Key.FirstName + " " + e.Key.LastName, Count = e.Count() }).ToList();
            }
        }


        [NotMapped]
        public List<Record> DepartmentEmployeeCount
        {
            get
            {
                return db.Employees.OrderBy(e => e.DepartmentId)
                    .GroupBy(e => e.Department)
                    .Select(e => new Record() { Label = e.Key.DepartmentName, Count = e.Count() }).ToList();
            }
        }

        private int getWeek(DateTime date)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }

    public class Record
    {
        [NotMapped]
        public string Label { get; set; }

        [NotMapped]
        public int Count { get; set; }
    }
}