using Core.BusinessObject;

namespace Core.Services
{
    public interface IEmployeeServices
    {
        public OperationResult Add(EmployeeL employee);
        OperationResult Update(EmployeeL employee);
        public List<EmployeeL> GetAll();

        public EmployeeL GetByName(string name);
        public void Delete(int id);
        public EmployeeL getById(int id);

        public EmployeeL GetByEmailAndPassword(string email, string password);
        public EmployeeL GetByEmailPasswordCompanyId(string email, string password, int companyid);
        public string GetEducationDetails(int id);
        public List<EmployeeL> GetAllByCompanyId(int companyId);
    }
}
