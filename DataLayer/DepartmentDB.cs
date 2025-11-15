using Core.BusinessObject;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace DataLayer
{
    public class DepartmentDB
    {

        public static void Add(Department department)
        {
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Insert INTO Department(Name, Description,  CreatedBy, UpdatedBy, IPAddress, CreatedOnUTC, UpdatedOnUTC) values (@DepartmentName, @Description, @CreatedBy, @UpdatedBy, @IPAddress, @CreatedOnUTC, @UpdatedOnUTC); SELECT LAST_INSERT_ID()");
            db.AddInParameter(cmd, "DepartmentName", DbType.String, department.DepartmentName);
            db.AddInParameter(cmd, "Description", DbType.String, department.Description);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, department.CreatedBy);
            db.AddInParameter(cmd, "UpdatedBy", DbType.String, department.UpdatedBy);
            db.AddInParameter(cmd, "IPAddress", DbType.String, department.IPAddress);
            db.AddInParameter(cmd, "CreatedOnUTC", DbType.DateTime, department.CreatedOnUTC);
            db.AddInParameter(cmd, "UpdatedOnUTC", DbType.DateTime, department.UpdatedOnUTC);
            department.Id = int.Parse(db.ExecuteScalar(cmd).ToString());
        }

        public static void Update(Department department)
        {
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Update Department set Name=@DepartmentName, Description=@Description, CreatedBy=@CreatedBy,  UpdatedBy=@UpdatedBy, IPAddress=@IPAddress, CreatedOnUTC=@CreatedOnUTC, UpdatedOnUTC=@UpdatedOnUTC  where ID=@ID");
            db.AddInParameter(cmd, "ID", DbType.Int32, department.Id);
            db.AddInParameter(cmd, "DepartmentName", DbType.String, department.DepartmentName);
            db.AddInParameter(cmd, "Description", DbType.String, department.Description);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, department.CreatedBy);
            db.AddInParameter(cmd, "UpdatedBy", DbType.String, department.UpdatedBy);
            db.AddInParameter(cmd, "IPAddress", DbType.String, department.IPAddress);
            db.AddInParameter(cmd, "CreatedOnUTC", DbType.DateTime, department.CreatedOnUTC);
            db.AddInParameter(cmd, "UpdatedOnUTC", DbType.DateTime, department.UpdatedOnUTC);
            db.ExecuteNonQuery(cmd);
        }

        public static void Delete(int id)
        {
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Delete FROM Department where Id = @ID");
            db.AddInParameter(cmd, "ID", DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
        }

        public static List<Department> GetAll()
        {
            List<Department> departmentList = new List<Department>();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * FROM Department");
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                Department department = new Department(reader);
                departmentList.Add(department);

            }

            return departmentList;

        }

        public static Department GetById(int ID)
        {
            Department department = new Department();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * From Department where ID=@ID");
            db.AddInParameter(cmd, "ID", DbType.Int32, ID);
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                department = new Department(reader);

            }
            return department;
        }
        public static List<Department> GetAllName()
        {
            List<Department> departmentList = new List<Department>();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select ID, Name, Description From Department");
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                Department department = new Department(reader);
                departmentList.Add(department);

            }

            return departmentList;
        }
    }
}
