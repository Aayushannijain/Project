using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;

namespace LearningPhase2.AppCode
{
    public class BaseModel : PageModel
    {
        public static Logger Log = LogManager.GetLogger("LearningPhase2");
        public void Success(string message)
        {
            TempData["SuccessMessage"] = message;
        }
        public void Warning(string message)
        {
            TempData["WarningMessage"] = message;
        }
    }
}
