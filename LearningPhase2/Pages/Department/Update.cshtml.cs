using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearningPhase2.Pages.Department
{
    [Authorize]
    public class UpdateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
