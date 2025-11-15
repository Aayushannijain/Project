using Core.BusinessObject;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NLog;
using System.Data;
using System.Data.Common;

namespace DataLayer
{
    public class EmployeeDB
    {
        public static Logger Log = LogManager.GetLogger("Data Layer");
        public static void Add(EmployeeL employee)
        {
            Log.Debug("Calling Add method to insert Employee Details in DB");
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Insert INTO Employee(name, email, Password, Status, phone, ProfilePic, CreatedBy, UpdatedBy, IPAddress, CreatedOnUTC, UpdatedOnUTC, EducationDetails, DOB) values (@Name, @Mail, @Password, @Status, @Phone, @ProfilePic, @CreatedBy, @UpdatedBy, @IPAddress, @CreatedOnUTC, @UpdatedOnUTC, @EducationDetails, @DOB); SELECT LAST_INSERT_ID()");
            db.AddInParameter(cmd, "Name", DbType.String, employee.Name);
            db.AddInParameter(cmd, "Mail", DbType.String, employee.Email);
            db.AddInParameter(cmd, "Password", DbType.String, employee.Password);
            db.AddInParameter(cmd, "Phone", DbType.String, employee.PhoneNumber);
            db.AddInParameter(cmd, "Status", DbType.String, employee.Status);
            db.AddInParameter(cmd, "DOB", DbType.DateTime, employee.DOB);
            db.AddInParameter(cmd, "ProfilePic", DbType.String, employee.ProfilePic);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, employee.CreatedBy);
            db.AddInParameter(cmd, "UpdatedBy", DbType.String, employee.UpdatedBy);
            db.AddInParameter(cmd, "IPAddress", DbType.String, employee.IPAddress);
            db.AddInParameter(cmd, "CreatedOnUTC", DbType.DateTime, employee.CreatedOnUTC);
            db.AddInParameter(cmd, "UpdatedOnUTC", DbType.DateTime, employee.UpdatedOnUTC);
            db.AddInParameter(cmd, "EducationDetails", DbType.String, employee.EducationDetails);
            employee.Id = int.Parse(db.ExecuteScalar(cmd).ToString());
            Log.Debug("Called Add method to insert Employee Details in DB");
        }

        public static void Update(EmployeeL employee)
        {
            Log.Debug("Calling Update method to insert Employee Details in DB");
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Update Employee set Name=@Name, Email=@Mail,Status=@Status, Phone=@Phone,  DepartmentID = @DepartmentID, DOB=@DOB, ProfilePic=@ProfilePic, CreatedBy=@CreatedBy,  UpdatedBy=@UpdatedBy, IPAddress=@IPAddress, CreatedOnUTC=@CreatedOnUTC, UpdatedOnUTC=@UpdatedOnUTC, EducationDetails=@EducationDetails  where Id=@Id");
            db.AddInParameter(cmd, "Id", DbType.Int32, employee.Id);
            db.AddInParameter(cmd, "Name", DbType.String, employee.Name);
            db.AddInParameter(cmd, "Mail", DbType.String, employee.Email);
            db.AddInParameter(cmd, "Password", DbType.String, employee.Password);
            db.AddInParameter(cmd, "Phone", DbType.String, employee.PhoneNumber);
            db.AddInParameter(cmd, "DepartmentID", DbType.Int32, employee.DepartmentID);
            db.AddInParameter(cmd, "DOB", DbType.DateTime, employee.DOB);
            db.AddInParameter(cmd, "ProfilePic", DbType.String, employee.ProfilePic);
            db.AddInParameter(cmd, "Status", DbType.String, employee.Status);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, employee.CreatedBy);
            db.AddInParameter(cmd, "UpdatedBy", DbType.String, employee.UpdatedBy);
            db.AddInParameter(cmd, "IPAddress", DbType.String, employee.IPAddress);
            db.AddInParameter(cmd, "CreatedOnUTC", DbType.DateTime, employee.CreatedOnUTC);
            db.AddInParameter(cmd, "UpdatedOnUTC", DbType.DateTime, employee.UpdatedOnUTC);
            db.AddInParameter(cmd, "EducationDetails", DbType.String, employee.EducationDetails);
            db.ExecuteNonQuery(cmd);
            Log.Debug("Called Update method to insert Employee Details in DB");
        }
        public static void Delete(int id)
        {
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Delete FROM Employee where Id = @id");
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
        }
        public static EmployeeL GetByName(string name)
        {
            EmployeeL employee = null;
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * FROM Employee where Name = @name");
            db.AddInParameter(cmd, "name", DbType.String, name);
            IDataReader reader = db.ExecuteReader(cmd);

            while (reader.Read())
            {
                employee = new EmployeeL(reader);
            }
            return employee;
        }

        public static List<EmployeeL> GetAll()
        {
            Log.Debug("Fetching record from DB");
            List<EmployeeL> employeeList = new List<EmployeeL>();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select e.*, d.Name AS DepartmentName, d.Description AS DepartmentDescription FROM Employee e LEFT JOIN Department d ON e.DepartmentId = d.ID ");
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                EmployeeL employee = new EmployeeL(reader);
                employee.DepartmentName = DBNull.Value != reader["DepartmentName"] ? (string)reader["DepartmentName"] : default;
                employee.DepartmentDescription = DBNull.Value != reader["DepartmentDescription"] ? (string)reader["DepartmentDescription"] : default;
                employeeList.Add(employee);
            }
            Log.Debug($"Fetched records from DB count - {employeeList.Count}");
            return employeeList;
        }
        public static EmployeeL GetByEmail(string email)
        {
            EmployeeL employee = null;
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd2 = db.GetSqlStringCommand($"Select * FROM Employee where email = @Email");
            db.AddInParameter(cmd2, "Email", DbType.String, email);
            IDataReader reader = db.ExecuteReader(cmd2);

            while (reader.Read())
            {
                employee = new EmployeeL(reader);
            }

            return employee;
        }

        public static EmployeeL GetByEmailAndPassword(string email, string password)
        {
            EmployeeL employee = null;
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd2 = db.GetSqlStringCommand($"Select * FROM Employee where email = @Email and Password=@Password");
            db.AddInParameter(cmd2, "Email", DbType.String, email);
            db.AddInParameter(cmd2, "Password", DbType.String, password);
            IDataReader reader = db.ExecuteReader(cmd2);

            while (reader.Read())
            {
                employee = new EmployeeL(reader);
            }

            return employee;
        }

        public static EmployeeL GetByEmailPasswordCompanyId(string email, string password, int companyid)
        {
            Log.Debug($"Calling this method in DataLayer to fetch Employee data from DB for Email:{email}, Password:{password} and CompanyId:{companyid}");
            EmployeeL employee = null;
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd2 = db.GetSqlStringCommand($"Select * FROM Employee where email = @Email and Password=@Password and CompanyId=@CompanyId");
            db.AddInParameter(cmd2, "Email", DbType.String, email);
            db.AddInParameter(cmd2, "Password", DbType.String, password);
            db.AddInParameter(cmd2, "CompanyId", DbType.Int32, companyid);
            IDataReader reader = db.ExecuteReader(cmd2);

            while (reader.Read())
            {
                employee = new EmployeeL(reader);
            }
            Log.Debug($"Calling this method in DataLayer to fetch Employee data from DB for Email:{email}, Password:{password} and CompanyId:{companyid}");
            return employee;
        }



        public static EmployeeL GetById(int id)
        {
            EmployeeL employee = null;
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * From Employee Where id = @id");
            db.AddInParameter(cmd, "id", DbType.Int32, id);
            IDataReader reader = db.ExecuteReader(cmd);

            while (reader.Read())
            {
                employee = new EmployeeL(reader);
            }
            return employee;
        }

        //public static List<EmployeeL> GetAllWithDepartment(int id)
        //{
        //    List<EmployeeL> employeeList = new List<EmployeeL>();
        //    Database db = ConnectionFactory.CreateDataBase();
        //    DbCommand cmd = db.GetSqlStringCommand($" SELECT  e.*, d.Name AS DepartmentName, d.Description AS DepartmentDescription FROM Employee e JOIN Department d ON e.DepartmentId = d.ID WHERE e.DepartmentId = @id");
        //    db.AddInParameter(cmd, "@id", DbType.Int32, id);
        //    IDataReader reader = db.ExecuteReader(cmd);
        //    while (reader.Read())
        //    {
        //        EmployeeL employee = new EmployeeL(reader);
        //        employeeList.Add(employee);

        //    }

        //    return employeeList;
        //}

        public static string EducationDetails(int employeeId)
        {
            Log.Debug("Fetching education column data from DB");
            string educationDetail = null;
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select EducationDetails From Employee Where id = @id");
            db.AddInParameter(cmd, "id", DbType.Int32, employeeId);
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                educationDetail = reader["EducationDetails"] != DBNull.Value
                ? reader["EducationDetails"].ToString()
                : null;
            }
            Log.Debug("Fetched education column data from DB");
            return educationDetail;
        }

        public static List<EmployeeL> GetAllByCompanyId(int companyId)
        {
            Log.Debug("Fetching record from DB after filtering through CompanyId");
            List<EmployeeL> employeeList = new List<EmployeeL>();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select e.*, d.Name AS DepartmentName, d.Description AS DepartmentDescription FROM Employee e LEFT JOIN Department d ON e.DepartmentId = d.ID Where e.CompanyId=@CompanyId ");
            db.AddInParameter(cmd, "CompanyId", DbType.Int32, companyId);
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                EmployeeL employee = new EmployeeL(reader);
                employee.DepartmentName = DBNull.Value != reader["DepartmentName"] ? (string)reader["DepartmentName"] : default;
                employee.DepartmentDescription = DBNull.Value != reader["DepartmentDescription"] ? (string)reader["DepartmentDescription"] : default;
                employeeList.Add(employee);
            }
            Log.Debug($"Fetched records from DB count - {employeeList.Count}");
            return employeeList;
        }
    }
}
