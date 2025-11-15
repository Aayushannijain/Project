using Core;
using Core.BusinessObject;
using Core.Services;
using LearningPhase2.AppCode;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeviceLayer;
using System.ComponentModel.DataAnnotations;
using static Core.OperationResult;

namespace LearningPhase2.Pages.EmployeeNTier
{
    [BindProperties]
    [TypeFilter(typeof(LoginCheckFilter))]
    public class EducationModel : BaseModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string College { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string University { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Percentage { get; set; }
        public List<Education> Educationlist { get; set; } = new List<Education>();
        public string EducationId { get; set; }
        public void OnGet()
        {
            Log.Debug("Calling OnGetEducationDetails method");

            ViewData["currentEmployeeId"] = HttpContext.Session.GetInt32("EmployeeId");
            int? loggedInId = ViewData["currentEmployeeId"] as int?;
            if (loggedInId == null)
            {
                return;
            }

            IEmployeeServices employeeManager = new EmployeeManager();
            var EducationList = employeeManager.GetEducationDetails(loggedInId.Value);
            if (!string.IsNullOrWhiteSpace(EducationList))
            {
                Log.Debug("Deserializing Json string  Data to Object type");
                Educationlist = JsonConvert.DeserializeObject<List<Education>>(EducationList);


            }

            Log.Debug("Completed OnGetEducationDetails method");
            return; // Returns the Razor page with data bound    
        }
        /// <summary>
        /// This Onpost method is used to add education details
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            ViewData["currentEmployeeId"] = HttpContext.Session.GetInt32("EmployeeId");
            int? loggedInId = ViewData["currentEmployeeId"] as int?;
            if (loggedInId == null)
            {
                Warning("Unauthorised or session expired");
                return RedirectToPage("Education");
            }

            var educationDetails = new Education
            {
                EducationId = Guid.NewGuid().ToString(),
                Degree = Degree,
                Specialization = Specialization,
                University = University,
                College = College,
                Percentage = Percentage,
            };

            Log.Debug($"Calling AddEmployee OnPost method");
            IEmployeeServices employeeManager = new EmployeeManager();
            var employee = employeeManager.getById(loggedInId.Value);

            if (employee == null)
            {
                Warning("Employee Not Found");
                return RedirectToPage("Education");

            }
            Educationlist = new List<Education>();
            if (!string.IsNullOrWhiteSpace(employee.EducationDetails))
            {
                Educationlist = JsonConvert.DeserializeObject<List<Education>>(employee.EducationDetails);
            }

            Educationlist.Add(educationDetails);

            employee.EducationDetails = JsonConvert.SerializeObject(Educationlist);
            OperationResult operationResult = employeeManager.Update(employee);
            if (operationResult.Status == (int)OperationStatus.Success)
            {
                Success(SLConstants.Messages.EDUCATIONDETAILS_ADDED_SUCCESS_MESSAGE);
            }
            else
            {
                Warning(SLConstants.Messages.EDUCATIONDETAILS_ADDED_FAILURE_MESSAGE);
                return Page();
            }
            Log.Debug($"Called EducationDeatils OnPost method");
            return RedirectToPage("Education");
        }
        /// <summary>
        /// This OnGetEdit method returns the employee details in the form fields
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult OnGetEdit(string id)
        {
            Log.Debug($"Calling Education OnPostEdit method");
            ViewData["currentEmployeeId"] = HttpContext.Session.GetInt32("EmployeeId");
            int? loggedInId = ViewData["currentEmployeeId"] as int?;
            if (loggedInId == null)
            {

                return new JsonResult(new { success = false, message = SLConstants.Messages.SESSION_EXPIRED_MESSAGE });
            }


            IEmployeeServices employeeManager = new EmployeeManager();
            var EducationJson = employeeManager.GetEducationDetails(loggedInId.Value);
            List<Education> Educationlist = new List<Education>();
            if (!string.IsNullOrWhiteSpace(EducationJson))
            {
                Educationlist = JsonConvert.DeserializeObject<List<Education>>(EducationJson);
            }
            bool isUpdated = false;
            foreach (var edu in Educationlist)
            {
                if (string.IsNullOrWhiteSpace(edu.EducationId))
                {
                    edu.EducationId = Guid.NewGuid().ToString();
                    isUpdated = true;
                }
            }
            if (isUpdated)
            {
                var employee = employeeManager.getById(loggedInId.Value);
                employee.EducationDetails = JsonConvert.SerializeObject(Educationlist);
                employeeManager.Update(employee);
            }

            var existingEduList = Educationlist.FirstOrDefault(e => e.EducationId == id);
            if (existingEduList == null)
            {
                return new JsonResult(new { success = false, message = SLConstants.Messages.RECORD_NOT_FOUND_MESSAGE });
            }
            Log.Debug($"Called Education OnPostEdit method");
            return new JsonResult(new
            {
                success = true,
                educationId = existingEduList.EducationId,
                degree = existingEduList.Degree,
                specialization = existingEduList.Specialization,
                college = existingEduList.College,
                university = existingEduList.University,
                percentage = existingEduList.Percentage
            });


        }
        /// <summary>
        /// This method is used to update education details in educationdetails of Employee column
        /// </summary>
        /// <returns></returns>
        public JsonResult OnPostUpdate()
        {
            Log.Debug($"Calling Education OnPostUpdate method");
            ViewData["currentEmployeeId"] = HttpContext.Session.GetInt32("EmployeeId");
            int? loggedInId = ViewData["currentEmployeeId"] as int?;
            if (loggedInId == null)
            {
                return new JsonResult(new { success = false, message = SLConstants.Messages.SESSION_EXPIRED_MESSAGE });
            }
            IEmployeeServices employeeManager = new EmployeeManager();
            var employee = employeeManager.getById(loggedInId.Value);
            var EducationJson = employeeManager.GetEducationDetails(loggedInId.Value);
            List<Education> Educationlist = new List<Education>();
            if (!string.IsNullOrWhiteSpace(EducationJson))
            {

                Educationlist = JsonConvert.DeserializeObject<List<Education>>(EducationJson);

            }

            var existingEdu = Educationlist.FirstOrDefault(e => e.EducationId == EducationId);
            if (existingEdu == null)
            {
                return new JsonResult(new { success = false, message = SLConstants.Messages.RECORD_NOT_FOUND_MESSAGE });
            }
            existingEdu.Degree = Degree;
            existingEdu.Specialization = Specialization;
            existingEdu.College = College;
            existingEdu.University = University;
            existingEdu.Percentage = Percentage;
            employee.EducationDetails = JsonConvert.SerializeObject(Educationlist);
            OperationResult operationResult = employeeManager.Update(employee);
            if (operationResult.Status == (int)OperationStatus.Success)
            {
                Success(SLConstants.Messages.EDUCATIONDETAILS_UPDATED_SUCCESS_MESSAGE);
            }



            return new JsonResult(new { success = false, message = SLConstants.Messages.EDUCATIONDETAILS_UPDATED_FAILURE_MESSAGE });


        }
        /// <summary>
        /// This method Deletes the education detail for particular education id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult OnGetDelete(string Id)
        {
            Log.Debug("Calling OnGetDelete method in Education Page");
            Log.Debug("EducationId received: " + EducationId);

            ViewData["currentEmployeeId"] = HttpContext.Session.GetInt32("EmployeeId");
            int? loggedInId = ViewData["currentEmployeeId"] as int?;
            if (loggedInId == null)
            {
                return new JsonResult(new { success = false, message = SLConstants.Messages.SESSION_EXPIRED_MESSAGE });
            }
            IEmployeeServices employeeManager = new EmployeeManager();
            var employee = employeeManager.getById(loggedInId.Value);
            var EducationJson = employeeManager.GetEducationDetails(loggedInId.Value);
            List<Education> Educationlist = new List<Education>();
            if (!string.IsNullOrWhiteSpace(EducationJson))
            {

                Educationlist = JsonConvert.DeserializeObject<List<Education>>(EducationJson);

            }
            int removedCount = Educationlist.RemoveAll(e => e.EducationId.ToString() == Id);
            if (removedCount == 0)
            {
                return new JsonResult(new { success = false, message = SLConstants.Messages.RECORD_NOT_FOUND_MESSAGE });
            }

            // 5. Serialize updated list
            employee.EducationDetails = JsonConvert.SerializeObject(Educationlist);

            Log.Debug("Calling Update method on AddEducationDetails Page");
            // 6. Save changes
            OperationResult operationResult = employeeManager.Update(employee);

            // 7. Return outcome
            if (operationResult.Status == (int)OperationStatus.Success)
            {
                Log.Debug("Update method successfully called");
                Success(SLConstants.Messages.EDUCATIONDETAILS_DELETED_SUCCESS_MESSAGE);
                return new JsonResult(new { success = true });

            }
            else
            {
                Log.Debug("Failed to update delete employee's education detail");
                Warning(SLConstants.Messages.EDUCATIONDETAILS_DELETED_FAILURE_MESSAGE);
                return new JsonResult(new { success = false });
            }

        }

    }
}
