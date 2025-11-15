using Core;
using Core.BusinessObject;
using DataLayer;
using NLog;
using static Core.OperationResult;

namespace SeviceLayer
{
    public class DepartmentManager
    {
        public static Logger Log = LogManager.GetLogger("Service Layer");
        /// <summary>
        /// This method insert the departments details to the DB
        /// </summary>
        /// <param name="department"></param>
        public OperationResult Add(Department department)
        {
            //Department alreadyExistsDueToName = GetByName(department.DepartmentName);
            //if (alreadyExistsDueToName != null)
            //{
            //    return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EMPLOYEE_ADDED_FAILURE_NAME_UNIQUE, department);
            //}


            Log.Debug("Added department from service layer");
            DepartmentDB.Add(department);
            return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.DEPARTMENT_ADDED_SUCCESS_MESSAGE, department);
        }
        /// <summary>
        /// This method update the department details
        /// </summary>
        /// <param name="department"></param>
        public OperationResult Update(Department department)
        {
            //Department alreadyExists = GetByName(department.DepartmentName);
            //if (alreadyExists != null && alreadyExists.Id != employee.Id)
            //{
            //    return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EmployeeUpdatedFailureMessage, employee);
            //}
            Log.Debug("Added department from service layer");
            DepartmentDB.Update(department);
            return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.DEPARTMENT_UPDATED_SUCCESS_MESSAGE, department);

        }
        /// <summary>
        /// This method fetch all the department lists
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAll()
        {
            Log.Debug("Fetching department lists service layer");
            List<Department> departmentlist = DepartmentDB.GetAll();
            Log.Debug("Fetched department lists service layer");
            return departmentlist;

        }
        public List<Department> GetAllDepartmentName()
        {
            List<Department> departmentlist = DepartmentDB.GetAllName();
            return departmentlist;

        }
        /// <summary>
        /// This method delete the department for the particular id
        /// </summary>
        /// <param name="id"></param>
        public OperationResult Delete(int ID)
        {
            Log.Debug("Deleting department record from service layer");
            Department department = DepartmentDB.GetById(ID);
            DepartmentDB.Delete(ID);
            Log.Debug("Deleted  department record from service layer");
            return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.DEPARTMENT_DELETED_SUCCESS_MESSAGE, department);
        }
        /// <summary>
        /// This method fetch the department for the particular id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Department GetById(int id)
        {
            Log.Debug("Fetching department record from service layer for particular id");
            Department department = DepartmentDB.GetById(id);
            Log.Debug("Fetched department record from service layer for particular id");
            return department;
        }
    }
}
