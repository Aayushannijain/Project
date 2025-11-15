using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;

namespace LearningPhase2.Pages.Employee
{
    [Authorize]
    [BindProperties]
    public class AddEmployeeModel : PageModel
    {

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> Departments { get; set; }
        public string Department { get; set; }
        public void OnGet()
        {
            List<string> items = CommonFunction.GetDepartments();
            Departments = items;
            Department = Constants.ITDepartment;
        }
        public IActionResult OnPost()
        {
            string myName = Name;
            string myEmail = Email;
            string number = PhoneNumber;

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("Local");
            DbCommand cmd = db.GetSqlStringCommand($"Insert INTO Employee(name, email, phone) values (@Name, @Mail, @Phone)");
            db.AddInParameter(cmd, "Name", DbType.String, Name);
            db.AddInParameter(cmd, "Mail", DbType.String, Email);
            db.AddInParameter(cmd, "Phone", DbType.String, PhoneNumber);
            db.ExecuteNonQuery(cmd);

            return RedirectToPage("List");
        }
    }
}
