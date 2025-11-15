using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Mvc;

namespace LearningPhase2.Pages.Employees
{
    public class ImagesModel : BaseModel
    {
        public IActionResult OnGet(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string path = $"C:\\Users\\Developer\\Documents\\Aayush\\ProfilePic\\{fileName}";
                return File(System.IO.File.ReadAllBytes(path), "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
