using Core;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using static Core.OperationResult;

namespace LearningPhase2.Pages.Department
{
    [Authorize]
    public class DepartmentListModel : BaseModel
    {
        public List<Core.BusinessObject.Department> DepartmentList { get; set; }

        public void OnGet()
        {
            Log.Debug("Calling Departmentlist OnGet method");
            DepartmentManager departmentManager = new DepartmentManager();
            DepartmentList = departmentManager.GetAll();
            Log.Debug("DepartmentList page OnGet method completed");
        }

        //public IActionResult OnGetByName(string name)
        //{
        //    DepartmentManager departmentManager = new DepartmentManager();
        //    EmployeeL employee = departmentManager.GetByName(name);
        //    return RedirectToPage("List");

        //}
        public IActionResult OnGetDelete(int id)
        {
            Log.Debug("Calling OnDelete method for particular id ");
            Core.BusinessObject.Department department = null;
            DepartmentManager departmentManager = new DepartmentManager();
            OperationResult operationResult = departmentManager.Delete(id);
            if (operationResult.Status == (int)OperationStatus.Success)
            {
                Log.Debug($"Delete method successful for id:{id} ");
                Success(operationResult.Message);
            }
            else
            {
                Warning(operationResult.Message);
                return Page();
            }
            Log.Debug($"Called OnDelete method for id:{id} ");
            return RedirectToPage("DepartmentList");


        }
    }
}
