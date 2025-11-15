using System.Data;


namespace Core.BusinessObject
{
    public class EmployeeL : BaseObject
    {

        public string Name { get; set; }

        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string DepartmentDescription { get; set; }
        public int DepartmentID { get; set; }
        public int CompanyId { get; set; }
        public string ProfilePic { get; set; }
        public string Password { get; set; }
        public string EducationDetails { get; set; }
        public DateTime DOB { get; set; }

        public EmployeeL()
        {
        }
        public EmployeeL(IDataReader reader) : base(reader)
        {

            Name = DBNull.Value != reader["Name"] ? (string)reader["Name"] : default;
            Email = DBNull.Value != reader["Email"] ? (string)reader["Email"] : default;
            PhoneNumber = DBNull.Value != reader["Phone"] ? (string)reader["Phone"] : default;
            Status = DBNull.Value != reader["Status"] ? (string)reader["Status"] : default;
            Password = DBNull.Value != reader["Password"] ? (string)reader["Password"] : default;
            DepartmentID = reader["DepartmentID"] != DBNull.Value ? (int)reader["DepartmentID"] : default;
            ProfilePic = DBNull.Value != reader["ProfilePic"] ? (string)reader["ProfilePic"] : default;
            EducationDetails = DBNull.Value != reader["EducationDetails"] ? (string)reader["EducationDetails"] : default;
            CompanyId = reader["CompanyId"] != DBNull.Value ? (int)reader["CompanyId"] : default;
            DOB = DBNull.Value != reader["DOB"] ? (DateTime)reader["DOB"] : default;

        }
    }
}
