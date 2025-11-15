using Core;
using Core.BusinessObject;
using Core.Services;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceLayer;
using System.ComponentModel.DataAnnotations;
using OperationStatus = Core.OperationResult.OperationStatus;


namespace LearningPhase2.Pages.EmployeeNTier
{
    [Authorize]
    [TypeFilter(typeof(LoginCheckFilter))]
    [BindProperties]
    public class AddEmployeeModel : BaseModel
    {

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(50, ErrorMessage = "Max 50 characters are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email ID")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        public string Password { get; set; }

        public string Status { get; set; }
        public List<string> StatusList { get; set; }

        public string PhoneNumber { get; set; }
        public string DOB { get; set; }
        public IFormFile ProfilePic { get; set; }
        public int EducationId { get; set; }

        public List<Core.BusinessObject.Department> Departments { get; set; }
        public int DepartmentId { get; set; }
        public void OnGet()
        {
            DepartmentManager departmentManager = new DepartmentManager();
            Departments = departmentManager.GetAll().ToList();
            StatusList = CommonFunction.GetStatusList();
        }
        public IActionResult OnPost()
        {
            Log.Debug($"Add employee OnPost method storing Form values");
            EmployeeL employee = new EmployeeL();
            employee.Name = Name;
            employee.Email = Email;
            employee.Password = Password;
            employee.DepartmentID = DepartmentId;
            employee.PhoneNumber = PhoneNumber;
            employee.Status = Status;
            employee.CreatedOnUTC = DateTime.UtcNow;
            employee.UpdatedOnUTC = DateTime.UtcNow;
            employee.CreatedBy = "System";
            employee.UpdatedBy = "System";
            DateTime dateOfBirth = DateTime.MinValue;
            DateTime.TryParse(DOB, out dateOfBirth);
            employee.DOB = dateOfBirth;
            employee.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            Log.Debug("Storing the image to the Filepath");
            //creating stream and defining path for storing the files in folders
            string FileName = $"{Guid.NewGuid()}_{ProfilePic.FileName}";
            string FilePath = $"C:\\Users\\Developer\\Documents\\Aayush\\ProfilePic\\{FileName}";
            using (Stream filestream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                ProfilePic.CopyTo(filestream);

            }
            employee.ProfilePic = FileName;

            Log.Debug(" image has been stored");

            Log.Debug($"Calling AddEmployee OnPost method");
            IEmployeeServices employeeManager = new EmployeeManager();
            OperationResult operationResult = employeeManager.Add(employee);
            if (operationResult.Status == (int)OperationStatus.Success)
            {
                Success(operationResult.Message);
            }
            else
            {
                Warning(operationResult.Message);
                return Page();
            }
            Log.Debug($"Called AddEmployee OnPost method");
            return RedirectToPage("List");
        }
    }
}
