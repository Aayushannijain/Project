using Core.BusinessObject;
using Core.Services;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using System.Data;

namespace LearningPhase2.Pages.EmployeeNTier
{
    [Authorize]
    public class ListModel : BaseModel
    {
        public List<EmployeeL> EmployeeList { get; set; }

        //public List<Core.BusinessObject.Department> Departments { get; set; }

        public IActionResult OnGet()
        {
            Log.Debug("Calling list OnGet method");
            ViewData["currentEmployee"] = HttpContext.Session.GetString("Current Employee");
            ViewData["currentEmployeeId"] = HttpContext.Session.GetInt32("EmployeeId");
            ViewData["currentCompanyId"] = HttpContext.Session.GetInt32("CompanyID");
            ViewData["currentCompanyName"] = HttpContext.Session.GetString("CompanyName");
            Log.Debug("Getting CompanyId to fetch the employees for that particular ID");
            int? companyId = HttpContext.Session.GetInt32("CompanyID");
            if (companyId == null)
            {
                return RedirectToPage("/Login");
            }
            //DepartmentManager departmentManager = new DepartmentManager();
            //Departments = departmentManager.GetAll();
            IEmployeeServices employeeManager = new EmployeeManager();
            EmployeeList = employeeManager.GetAllByCompanyId((int)companyId);
            Log.Debug("List page OnGet method completed");
            return Page();

        }

        public IActionResult OnGetByName(string name)
        {
            Log.Debug($"Calling list On Get By Name:{name} method");
            IEmployeeServices employeeManager = new EmployeeManager();
            EmployeeL employee = employeeManager.GetByName(name);
            Log.Debug($"Called list On Get By Name:{name} method");
            return RedirectToPage("List");

        }
        public IActionResult OnGetDelete(int id)
        {
            Log.Debug($"Calling list On Get By Delete:{id} method");
            IEmployeeServices employeeManager = new EmployeeManager();
            employeeManager.Delete(id);
            Success(Messages.EmployeeDeletedSuccesMessage);
            Log.Debug($"Called list On Get By Delete:{id} method");
            return RedirectToPage("List");
        }
    }
}
