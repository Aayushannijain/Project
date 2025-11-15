using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;

namespace LearningPhase2.Pages.Employees
{
    [Authorize]
    [BindProperties]
    public class UpdateModel : PageModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }


        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }
        }
        public void OnGet(int id)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("Local");
            DbCommand cmd = db.GetSqlStringCommand($"Select * FROM Employee where id = @id");
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            IDataReader reader = db.ExecuteReader(cmd);
            reader.Read();
            Employee employee = new Employee();


            employee.Id = DBNull.Value != reader["id"] ? (int)reader["id"] : default(int);
            employee.Name = DBNull.Value != reader["Name"] ? (string)reader["Name"] : default(string);
            employee.Email = DBNull.Value != reader["Email"] ? (string)reader["Email"] : default(string);
            employee.PhoneNumber = DBNull.Value != reader["Phone"] ? (string)reader["Phone"] : default(string);

            Name = employee.Name;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            Id = employee.Id;

        }

        public IActionResult OnPost()
        {
            string myName = Name;
            string myEmail = Email;
            string number = PhoneNumber;

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("Local");
            DbCommand cmd = db.GetSqlStringCommand($"Update Employee set Name=@Name, Email=@Mail, Phone=@Phone where Id=@Id");
            db.AddInParameter(cmd, "Id", DbType.Int32, Id);
            db.AddInParameter(cmd, "Name", DbType.String, Name);
            db.AddInParameter(cmd, "Mail", DbType.String, Email);
            db.AddInParameter(cmd, "Phone", DbType.String, PhoneNumber);
            db.ExecuteNonQuery(cmd);

            return RedirectToPage("List");
        }
    }
}
