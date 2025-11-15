using Core;
using Core.BusinessObject;
using DataLayer;
using NLog;
using static Core.OperationResult;

namespace SeviceLayer
{
    public class CompanyManager
    {
        public static Logger Log = LogManager.GetLogger("Service Layer");

        public OperationResult AddCompany(Company company)
        {

            //Department alreadyExistsDueToName = GetByName(department.DepartmentName);
            //if (alreadyExistsDueToName != null)
            //{
            //    return new OperationResult((int)OperationStatus.Failure, SLConstants.Messages.EMPLOYEE_ADDED_FAILURE_NAME_UNIQUE, department);
            //}


            Log.Debug("Added department from service layer");
            CompanyDB.AddCompany(company);
            return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.DEPARTMENT_ADDED_SUCCESS_MESSAGE, company);

        }
        //public OperationResult UpdateCompany(Company company)
        //{
        //    CompanyDB.UpdateCompany(company);
        //    return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.DEPARTMENT_ADDED_SUCCESS_MESSAGE, company);
        //}

        public List<Company> GetAllCompanies()
        {
            Log.Debug("Fetching department lists service layer");
            List<Company> companylist = CompanyDB.GetAllCompanies();
            Log.Debug("Fetched department lists service layer");
            return companylist;

        }

        //public OperationResult DeleteCompany(int ID)
        //{
        //    Log.Debug("Deleting department record from service layer");
        //    Company company = CompanyDB.GetCompanyById(ID);
        //    CompanyDB.DeleteCompany(ID);
        //    Log.Debug("Deleted  department record from service layer");
        //    return new OperationResult((int)OperationStatus.Success, SLConstants.Messages.DEPARTMENT_DELETED_SUCCESS_MESSAGE, company);
        //}

        public Company GetCompanyById(int id)
        {
            Log.Debug("Fetching company record from service layer for particular id");
            Company company = CompanyDB.GetCompanyById(id);
            Log.Debug("Fetched company record from service layer for particular id");
            return company;
        }

        public Company GetByCompanySubdomain(string domain)
        {
            Log.Debug("Fetching company record from service layer for particular domain");
            Company company = CompanyDB.GetByCompanySubdomain(domain);
            Log.Debug("Fetched company record from service layer for particular domain");
            return company;
        }
    }
}
