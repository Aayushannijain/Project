using Core.BusinessObject;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using SeviceLayer;

namespace LearningPhase2.Pages.Companies
{
    [Authorize]
    public class CompanyListModel : BaseModel
    {

        public List<Company> CompanyList { get; set; }

        public void OnGet()
        {
            Log.Debug("Calling Departmentlist OnGet method");
            CompanyManager companymanager = new CompanyManager();
            CompanyList = companymanager.GetAllCompanies();
            Log.Debug("DepartmentList page OnGet method completed");
        }

        //public IActionResult OnGetDelete(int id)
        //{
        //    Log.Debug("Calling OnDelete method for particular id ");
        //    Core.BusinessObject.Department department = null;
        //    CompanyManager companymanager = new CompanyManager();
        //    OperationResult operationResult = companymanager.DeleteCompany(id);
        //    if (operationResult.Status == (int)OperationStatus.Success)
        //    {
        //        Log.Debug($"Delete method successful for id:{id} ");
        //        Success(operationResult.Message);
        //    }
        //    else
        //    {
        //        Warning(operationResult.Message);
        //        return Page();
        //    }
        //    Log.Debug($"Called OnDelete method for id:{id} ");
        //    return RedirectToPage("CompanyList");


        //}
    }
}
