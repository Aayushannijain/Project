using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace LearningPhase2.Pages.Employees
{
    [Authorize]
    public class ListModel : PageModel
    {
        public List<Employee> EmployeeList { get; set; }
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }
        }
        public void OnGet()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("Local");
            DbCommand cmd = db.GetSqlStringCommand($"Select * FROM Employee");
            IDataReader reader = db.ExecuteReader(cmd);
            List<Employee> employeeList = new List<Employee>();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = DBNull.Value != reader["Id"] ? (int)reader["Id"] : default(int);
                employee.Name = DBNull.Value != reader["Name"] ? (string)reader["Name"] : default(string);
                employee.Email = DBNull.Value != reader["Email"] ? (string)reader["Email"] : default(string);
                employee.PhoneNumber = DBNull.Value != reader["Phone"] ? (string)reader["Phone"] : default(string);
                employeeList.Add(employee);

            }
            EmployeeList = employeeList;
        }

        public IActionResult OnGetByName(string name)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("Local");
            DbCommand cmd = db.GetSqlStringCommand($"Select * FROM Employee where Name = @name");
            db.AddInParameter(cmd, "name", DbType.String);
            IDataReader reader = db.ExecuteReader(cmd);
            Employee employee = new Employee();
            reader.Read();
            employee.Id = DBNull.Value != reader["Id"] ? (int)reader["Id"] : default(int);
            employee.Name = DBNull.Value != reader["Name"] ? (string)reader["Name"] : default(string);
            employee.Email = DBNull.Value != reader["Email"] ? (string)reader["Email"] : default(string);
            employee.PhoneNumber = DBNull.Value != reader["Phone"] ? (string)reader["Phone"] : default(string);

            return RedirectToPage("List");

        }
        public IActionResult OnGetDelete(int id)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("Local");
            DbCommand cmd = db.GetSqlStringCommand($"Delete FROM Employee where Id = @id");
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
            return RedirectToPage("List");
        }
    }
}
