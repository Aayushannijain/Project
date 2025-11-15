using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LearningPhase2.Pages.Account
{
    public class LogoutModel : BaseModel
    {
        public IActionResult OnGet()
        {
            Log.Debug("Logout.cshtml.cs onGet method executing");
            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext).Wait();
            return RedirectToPage("Login");
        }
    }
}
