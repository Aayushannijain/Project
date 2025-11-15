using Core;
using Core.BusinessObject;
using Core.Services;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LearningPhase2.Pages.EmployeeNTier
{
    [Authorize]
    [BindProperties]
    public class UpdateModel : BaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, ErrorMessage = "Max 50 characters are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email ID")]
        public string Email { get; set; }
        public string Password { get; set; }

        [StringLength(12, ErrorMessage = "Max 12 characters are allowed")]
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string DOB { get; set; }
        public List<string> StatusList { get; set; }
        public IFormFile ProfilePic { get; set; }
        public List<Core.BusinessObject.Department> Departments { get; set; }
        public int DepartmentId { get; set; }



        public IActionResult OnGet(int id)
        {
            int? companyId = HttpContext.Session.GetInt32("CompanyID");
            if (companyId == null)
            {
                Warning(SLConstants.Messages.SESSION_EXPIRED_MESSAGE);
                return Page();
            }
            DepartmentManager departmentManager = new DepartmentManager();
            Departments = departmentManager.GetAll().ToList();
            StatusList = CommonFunction.GetStatusList();
            IEmployeeServices employeeManager = new EmployeeManager();
            EmployeeL employee = employeeManager.getById(id);
            if (employee == null || employee.CompanyId != companyId)
            {
                Warning(SLConstants.Messages.EMPLOYEE_NOT_FOUND_MESSAGE);
                return Page();
            }
            Name = employee.Name;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            Status = employee.Status;
            DepartmentId = employee.DepartmentID;
            Password = employee.Password;

            return Page();
        }

        public IActionResult OnPost()
        {
            //int? companyId = HttpContext.Session.GetInt32("CompanyID");
            //if (companyId == null)
            //{
            //    Warning(SLConstants.Messages.SESSION_EXPIRED_MESSAGE);
            //    return Page();
            //}
            IEmployeeServices employeeManager = new EmployeeManager();
            EmployeeL employee = employeeManager.getById(Id);

            if (employee == null)
            {
                RedirectToPage("List");
            }

            employee.Id = Id;
            employee.Name = Name;
            employee.Email = Email;
            if (!string.IsNullOrEmpty(Password))
            {
                employee.Password = Password;
            }
            employee.DepartmentID = DepartmentId;
            employee.PhoneNumber = PhoneNumber;
            employee.Status = Status;
            employee.UpdatedBy = "System";
            employee.UpdatedOnUTC = DateTime.UtcNow;
            employee.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            DateTime dateOfBirth = DateTime.MinValue;
            DateTime.TryParse(DOB, out dateOfBirth);
            employee.DOB = dateOfBirth;

            Log.Debug("Storing the image to the Filepath in Update");
            string FileName = $"{Guid.NewGuid()}_{ProfilePic.FileName}";
            string FilePath = $"C:\\Users\\Developer\\Documents\\Aayush\\ProfilePic\\{FileName}";
            using (Stream filestream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                ProfilePic.CopyTo(filestream);

            }
            employee.ProfilePic = FileName;
            Log.Debug("Sent the image to the Filepath after update");

            OperationResult operationResult = employeeManager.Update(employee);
            if (employee.Id != default(int))
            {
                Success(operationResult.Message);
            }
            else
            {
                Warning(operationResult.Message);

            }
            return RedirectToPage("List");
        }
    }
}
