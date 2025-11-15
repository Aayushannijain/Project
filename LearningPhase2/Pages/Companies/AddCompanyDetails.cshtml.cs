using Core;
using Core.BusinessObject;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using System.ComponentModel.DataAnnotations;
using static Core.OperationResult;

namespace LearningPhase2.Pages.Companies
{
    [Authorize]
    [BindProperties]
    public class AddCompanyDetailsModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter the name")]
        public string OrganisationName { get; set; }
        [Required(ErrorMessage = "Please enter a Website")]
        public string Website { get; set; }
        [Required(ErrorMessage = "Please enter a Subdomain")]
        public string Subdomain { get; set; }

        public void OnGet()
        {
        }



        public IActionResult OnPost()
        {
            Log.Debug($"Calling Companies OnPost method to storing Form values");
            Company company = new Company();
            company.OrganisationName = OrganisationName;
            company.Website = Website;
            company.Subdomain = Subdomain;
            company.CreatedOnUTC = DateTime.UtcNow;
            company.UpdatedOnUTC = DateTime.UtcNow;
            company.CreatedBy = "System";
            company.UpdatedBy = "System";
            company.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            CompanyManager companiesManager = new CompanyManager();
            OperationResult operationResult = companiesManager.AddCompany(company);
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
            return RedirectToPage("CompanyList");
        }
    }
}
