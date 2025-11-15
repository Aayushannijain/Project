using System.Data;

namespace Core.BusinessObject
{
    public class Department : BaseObject
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }

        public Department() { }

        public Department(IDataReader reader) : base(reader)
        {

            DepartmentName = DBNull.Value != reader["Name"] ? (string)reader["Name"] : default;
            Description = DBNull.Value != reader["Description"] ? (string)reader["Description"] : default;
        }
    }
}
