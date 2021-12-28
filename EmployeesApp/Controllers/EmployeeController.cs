using Microsoft.AspNetCore.Mvc;
using EmployeesApp.Models;
namespace EmployeesApp.Controllers
{
    public class EmployeeController : Controller
    {
        HRDatabaseContext dbContext = new HRDatabaseContext();

        public IActionResult Index(string SortField, string CurrentSortField, string SortDirection)
        {
            var employees = GetEmployees();
            return View(this.SortEmployees(employees,SortField,CurrentSortField,SortDirection));
        }

        private List<Employee> GetEmployees()
        {
            var employees = (from employee in dbContext.Employees
                             join department in dbContext.Departments on employee.Departmentid equals department.DepartmentId
                             select new Employee
                             {
                                 EmployeeID = employee.EmployeeID,
                                 EmployeeName = employee.EmployeeName,
                                 EmployeeNumber = employee.EmployeeNumber,
                                 DOB = employee.DOB,
                                 HiringDate = employee.HiringDate,
                                 GrossSalary = employee.GrossSalary,
                                 NetSalary = employee.NetSalary,
                                 Departmentid = employee.Departmentid,
                                 DepartmentName = department.DepartmentName
                             }).ToList();

            return employees;
        }

        public IActionResult Create()
        {
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departmnets = this.dbContext.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int ID)
        {
            Employee data = this.dbContext.Employees.Where( e=> e.EmployeeID == ID).FirstOrDefault();
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View("Create",data);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Update(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departmnets = this.dbContext.Departments.ToList();
            return View("Create",model);
        }

        public IActionResult Delete(int ID)
        {
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeID == ID).FirstOrDefault();
            if(data != null)
            {
                dbContext.Employees.Remove(data);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private List<Employee> SortEmployees(List<Employee> employees, string sortField, string currentSortField, string sortDirection)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "EmployeeNumber";
                ViewBag.SortDirection = "Asc";
            }
            else
            {
                if (currentSortField == sortField)
                    ViewBag.SortDirection = sortDirection == "Asc" ? "Desc" : "Asc";
                else
                    ViewBag.SortDirection = "Asc";
                    ViewBag.SortField = sortField;
            }
            var propertyInfo = typeof(Employee).GetProperty(ViewBag.SortField);
            if (ViewBag.SortDirection == "Asc")
                employees = employees.OrderBy( e => propertyInfo.GetValue(e,null) ).ToList();
            else
                employees = employees.OrderByDescending( e => propertyInfo.GetValue(e, null) ).ToList();
            return employees;
        }
    }
}
