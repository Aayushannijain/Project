using Core;
using Core.BusinessObject;
using Core.Services;
using DataLayer;
using NLog;
using static Core.OperationResult;

namespace SeviceLayer
{
    public class EmployeeManager : IEmployeeServices
    {
        public static Logger Log = LogManager.GetLogger("Service Layer");
        /// <summary>
        /// This will add employee in DB
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>

        public OperationResult Add(EmployeeL employee)
        {
            Log.Debug("This method Adds employee details going through some checks");
            EmployeeL alreadyExistsDueToName = GetByName(employee.Name);
            if (alreadyExistsDueToName != null)
            {
                return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EMPLOYEE_ADDED_FAILURE_NAME_UNIQUE, employee);
            }

            if (!string.IsNullOrEmpty(employee.PhoneNumber) && employee.PhoneNumber.Length > 12)
            {
                return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EMPLOYEE_ADDED_FAILURE_PHONE_LENGTH, employee);
            }
            //This will check employee with same email id exists or not

            EmployeeL alreadyExists = GetByEmail(employee.Email);
            if (alreadyExists != null)
            {
                return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EmployeeAddedFailureMessage, employee);
            }
            Log.Debug("Added employee from servie layer");
            EmployeeDB.Add(employee);
            return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.EmployeeAddedSuccesMessage, employee);
        }
        /// <summary>
        /// This will update employee details in DB
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public OperationResult Update(EmployeeL employee)
        {
            Log.Debug("updating employee from service layer");
            EmployeeL alreadyExists = GetByEmail(employee.Email);
            if (alreadyExists != null && alreadyExists.Id != employee.Id)
            {
                return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EmployeeUpdatedFailureMessage, employee);
            }
            Log.Debug("updated employee from service layer");
            EmployeeDB.Update(employee);
            return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.EmployeeUpdatedSuccesMessage, employee);

        }
        /// <summary>
        /// This will Fetch the list of employees from DB
        /// </summary>
        /// <returns></returns>
        public List<EmployeeL> GetAll()
        {
            Log.Debug($"Fetching all records");
            List<EmployeeL> employeelist = EmployeeDB.GetAll();
            Log.Debug($"Fetched all records");
            return employeelist;

        }

        /// <summary>
        /// This will fetch the employee record for a specific name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EmployeeL GetByName(string name)
        {
            Log.Debug($"Fetching record with matching name");
            EmployeeL employee = EmployeeDB.GetByName(name);
            Log.Debug($"Fetched record with matching name");
            return employee;

        }
        /// <summary>
        /// This will check employee with the particular email in DB exists or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public EmployeeL GetByEmail(string email)
        {
            Log.Debug($"Fetching record with matching email id: {email}");
            EmployeeL employee = EmployeeDB.GetByEmail(email);
            Log.Debug($"Fetched record with matching email id: {email}");
            return employee;
        }
        /// <summary>
        /// This method fetch the employee details based on email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public EmployeeL GetByEmailAndPassword(string email, string password)
        {
            EmployeeL employee = EmployeeDB.GetByEmailAndPassword(email, password);
            return employee;
        }
        /// <summary>
        /// This method fetch the Employee details based on the company id and login details
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="companyid"></param>
        /// <returns></returns>
        public EmployeeL GetByEmailPasswordCompanyId(string email, string password, int companyid)
        {
            EmployeeL employee = EmployeeDB.GetByEmailPasswordCompanyId(email, password, companyid);
            return employee;
        }
        /// <summary>
        /// this method will delete the employee details based on the id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            Log.Debug($"Deleting record for the particular id");
            EmployeeDB.Delete(id);
            Log.Debug($"Deleted record for the particular id");
        }
        /// <summary>
        /// this method returns employee based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeL getById(int id)
        {
            EmployeeL employee = EmployeeDB.GetById(id);
            return employee;
        }

        public string GetEducationDetails(int employeeId)
        {
            string educationJson = EmployeeDB.EducationDetails(employeeId);
            return educationJson;
        }
        /// <summary>
        /// This method returns employee list for particular company Id from service layer
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<EmployeeL> GetAllByCompanyId(int companyId)
        {
            Log.Debug($"Fetching all Employee records for CompanyId:{companyId}");
            List<EmployeeL> employeelist = EmployeeDB.GetAllByCompanyId(companyId);
            Log.Debug($"Fetched all Employee records for CompanyId:{companyId}");
            return employeelist;
        }
    }
}
