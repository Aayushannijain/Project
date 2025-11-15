using Core;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using static Core.OperationResult;

namespace LearningPhase2.Pages.Department
{
    [Authorize]
    [BindProperties]
    public class AddDepartmentModel : BaseModel
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Core.BusinessObject.Department department = new Core.BusinessObject.Department();
            department.DepartmentName = DepartmentName;
            department.Description = Description;
            department.CreatedOnUTC = DateTime.UtcNow;
            department.UpdatedOnUTC = DateTime.UtcNow;
            department.CreatedBy = "System";
            department.UpdatedBy = "System";
            department.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            Log.Debug("Calling Onpost method to Add the department");
            DepartmentManager departmentManager = new DepartmentManager();
            OperationResult operationResult = departmentManager.Add(department);
            if (operationResult.Status == (int)OperationStatus.Success)
            {
                Success(operationResult.Message);
            }
            else
            {
                Warning(operationResult.Message);
                return Page();
            }
            Log.Debug("Called Onpost method to Add the department");
            return RedirectToPage("DepartmentList");
        }
    }
}
