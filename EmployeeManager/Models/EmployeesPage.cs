using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeManager
{
    public class EmployeesPage
    {
        private DataContext db = new DataContext();

        public string SearchString { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortAscending { get; set; }
        public int? PositionFilter { get; set; }
        public int? DepartmentFilter { get; set; }
        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }
        public int? StatusFilter { get; set; }
        public int? ShiftFilter { get; set; }
        public int? ManagerFilter { get; set; }

        public List<Employee> employeelist
        {
            get
            {
                var employeeList = db.Employees.Select(e => e);
                if (!string.IsNullOrEmpty(SearchString))
                {
                    employeeList = employeeList.Where(e => e.LastName.Contains(SearchString) || e.FirstName.Contains(SearchString));
                }
                if (PositionFilter != null)
                {
                    employeeList = employeeList.Where(e => e.PositionId == PositionFilter);
                }
                if (DepartmentFilter != null)
                {
                    employeeList = employeeList.Where(e => e.DepartmentId == DepartmentFilter);
                }
                if (StartDateFilter != null)
                {
                    employeeList = employeeList.Where(e => e.StartDate == StartDateFilter);
                }
                if (EndDateFilter != null)
                {
                    employeeList = employeeList.Where(e => e.EndDate == EndDateFilter);
                }
                if (StatusFilter != null)
                {
                    employeeList = employeeList.Where(e => e.EmploymentStatusId == StatusFilter);
                }
                if (ShiftFilter != null)
                {
                    employeeList = employeeList.Where(e => e.ShiftId == ShiftFilter);
                }
                if (ManagerFilter != null)
                {
                    employeeList = employeeList.Where(e => e.ManagerId == ManagerFilter);
                }
                if (!string.IsNullOrEmpty(SortColumn))
                {
                    ParameterExpression pe = Expression.Parameter(typeof(Employee), "e");
                    var typeArgement = new Type[] { employeeList.ElementType, typeof(string) };

                    var propertyType = typeof(Employee).GetProperty(SortColumn);
                    Expression lambdaBody = Expression.Property(pe, propertyType);

                    switch(SortColumn)
                    {
                        case "StartDate":
                            {
                                typeArgement = new Type[] { employeeList.ElementType, typeof(DateTime) };
                                break;
                            }
                        case "EndDate":
                            {
                                typeArgement = new Type[] { employeeList.ElementType, typeof(DateTime?) };
                                break;
                            }
                        case "EmploymentStatus":
                            {
                                lambdaBody = Expression.Property(lambdaBody, typeof(EmploymentStatus).GetProperty("Status"));
                                break;
                            }
                        case "Position":
                        case "Department":
                        case "Shift":
                            {
                                lambdaBody = Expression.Property(lambdaBody, Type.GetType("EmployeeManager." + propertyType.PropertyType.Name).GetProperty(SortColumn + "Name"));
                                break;
                            }
                        case "Manager":
                            {
                                lambdaBody = Expression.Property(lambdaBody, Type.GetType("EmployeeManager." + propertyType.PropertyType.Name).GetProperty("FirstName"));
                                break;
                            }
                    }
                    

                    LambdaExpression orderByExp = Expression.Lambda(lambdaBody, pe);
                    string methodName = IsSortAscending ? "OrderBy" : "OrderByDescending";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, typeArgement, employeeList.Expression, Expression.Quote(orderByExp));

                    employeeList = employeeList.Provider.CreateQuery<Employee>(resultExp);
                }

                return employeeList.ToList();
            }
        }

        public List<Position> PositionsList
        {
            get
            {
                return db.Employees.Where(e => e.PositionId != null)
                    .OrderBy(e => e.Position.PositionName)
                    .Select(e => e.Position)
                    .Distinct()
                    .ToList();
            }
        }

        public List<Department> DepartmentList
        {
            get
            {
                return db.Employees.Where(e => e.DepartmentId != null)
                    .OrderBy(e => e.Department.DepartmentName)
                    .Select(e => e.Department)
                    .Distinct()
                    .ToList();
            }
        }

        public List<DateTime> StartDateList
        {
            get
            {
                return db.Employees.Where(e => e.StartDate != null)
                    .OrderBy(e => e.StartDate)
                    .Select(e => e.StartDate)
                    .Distinct()
                    .ToList();
            }
        }

        public List<DateTime?> EndDateList
        {
            get
            {
                return db.Employees.Where(e => e.EndDate != null)
                    .OrderBy(e => e.EndDate)
                    .Select(e => e.EndDate)
                    .Distinct()
                    .ToList();
            }
        }

        public List<EmploymentStatus> StatusList
        {
            get
            {
                return db.Employees.Where(e => e.EmploymentStatusId != null)
                    .OrderBy(e => e.EmploymentStatus.Status)
                    .Select(e => e.EmploymentStatus)
                    .Distinct()
                    .ToList();
            }
        }

        public List<Shift> ShiftList
        {
            get
            {
                return db.Employees.Where(e => e.ShiftId != null)
                    .OrderBy(e => e.Shift.ShiftName)
                    .Select(e => e.Shift)
                    .Distinct()
                    .ToList();
            }
        }

        public List<Employee> ManagerList
        {
            get
            {
                return db.Employees.Where(e => e.ManagerId != null)
                    .OrderBy(e => e.Manager.FirstName)
                    .Select(e => e.Manager)
                    .Distinct()
                    .ToList();
            }
        }
    }
}