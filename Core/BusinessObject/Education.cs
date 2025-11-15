namespace Core.BusinessObject
{
    public class Education
    {
        public string EducationId { get; set; } = Guid.NewGuid().ToString();
        public string Degree { get; set; }
        public string Specialization { get; set; }
        public string College { get; set; }
        public string University { get; set; }
        public string Percentage { get; set; }
        public List<Education> Educationlist { get; set; }
        public Education() { }

        //public Education(IDataReader reader) 
        //{
        //    Degree = DBNull.Value != reader["Degree"] ? (string)reader["Degree"] : default;
        //    Specialization = DBNull.Value != reader["Specialization"] ? (string)reader["Specialization"] : default;
        //    College = DBNull.Value != reader["College"] ? (string)reader["College"] : default;
        //    University = DBNull.Value != reader["University"] ? (string)reader["University"] : default;
        //    Percentage = DBNull.Value != reader["Percentage"] ? (string)reader["Percentage"] : default;

        //}



    }
}
