using System.Data;

namespace Core.BusinessObject
{
    public class BaseObject
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreatedOnUTC { get; set; }

        public DateTime UpdatedOnUTC { get; set; }



        public BaseObject()
        {
        }
        public BaseObject(IDataReader reader)
        {
            Id = DBNull.Value != reader["id"] ? (int)reader["id"] : default;
            CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default;
            UpdatedBy = DBNull.Value != reader["UpdatedBy"] ? (string)reader["UpdatedBy"] : default;
            IPAddress = DBNull.Value != reader["IPAddress"] ? (string)reader["IPAddress"] : default;
            CreatedOnUTC = DBNull.Value != reader["CreatedOnUTC"] ? (DateTime)reader["CreatedOnUTC"] : default;
            UpdatedOnUTC = DBNull.Value != reader["UpdatedOnUTC"] ? (DateTime)reader["UpdatedOnUTC"] : default;

        }
    }
}
