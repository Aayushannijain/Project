using System.Data;

namespace Core.BusinessObject
{
    public class Company : BaseObject
    {
        public string OrganisationName { get; set; }
        public string Website { get; set; }
        public string Subdomain { get; set; }

        public Company() { }

        public Company(IDataReader reader) : base(reader)
        {
            OrganisationName = DBNull.Value != reader["OrganisationName"] ? (string)reader["OrganisationName"] : default;
            Website = DBNull.Value != reader["Website"] ? (string)reader["Website"] : default;
            Subdomain = DBNull.Value != reader["Subdomain"] ? (string)reader["Subdomain"] : default;
        }

    }
}
