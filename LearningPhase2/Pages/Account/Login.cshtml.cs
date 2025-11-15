using Core.BusinessObject;
using Core.Services;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace LearningPhase2.Pages.Account
{
    [BindProperties]
    public class LoginModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        public string Subdomain { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            //ViewData["currentSubdomain"] = HttpContext.Session.GetString("Subdomain");
            //string Subdomain = HttpContext.Session.GetString("Subdomain");

            Subdomain = HttpContext.Items["Subdomain"]?.ToString()
                ?? HttpContext.Session.GetString("Subdomain");
            CompanyManager companyManager = new CompanyManager();
            Company company = companyManager.GetByCompanySubdomain(Subdomain);
            if (company == null)
            {
                Warning("Invalid company subdomain.");
                return Page();
            }

            int companyId = company.Id;
            HttpContext.Session.SetInt32("CompanyId", companyId);

            string companyName = company.OrganisationName;

            IEmployeeServices employeeManager = new EmployeeManager();
            EmployeeL employee = employeeManager.GetByEmailPasswordCompanyId(Email, Password, companyId);
            if (employee == null)
            {
                Warning(SLConstants.Messages.INVALID_LOGIN_MESSAGE);
                return Page();
            }
            List<Claim> claims = new List<Claim>();
            Claim _Claim = new Claim(ClaimTypes.Sid, employee.Id.ToString(), ClaimValueTypes.Integer32);
            claims.Add(_Claim);

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationHttpContextExtensions.SignInAsync(HttpContext,
    CookieAuthenticationDefaults.AuthenticationScheme,
    new ClaimsPrincipal(identity),
    new AuthenticationProperties()
    {
        IsPersistent = false,
    });
            Log.Debug("Storing employee name as session data");
            string employeeName = employee.Name;
            HttpContext.Session.SetString("Current Employee", employeeName);

            Log.Debug($"Storing employee id as session data");
            int employeeId = employee.Id;
            HttpContext.Session.SetInt32("EmployeeId", employeeId);

            HttpContext.Session.SetInt32("CompanyID", companyId);
            HttpContext.Session.SetString("CompanyName", companyName);


            return RedirectToPage("/EmployeeNTier/List");
        }
    }
}
