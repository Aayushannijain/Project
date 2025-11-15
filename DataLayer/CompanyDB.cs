using Core.BusinessObject;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NLog;
using System.Data;
using System.Data.Common;

namespace DataLayer
{
    public static class CompanyDB
    {
        public static Logger Log = LogManager.GetLogger("Data Layer");
        /// <summary>
        /// This method add connection with the DB and Add company details
        /// </summary>
        /// <param name="company"></param>
        public static void AddCompany(Company company)
        {
            Log.Debug("Calling Add company method in data layer");
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Insert INTO Company(OrganisationName, Website,  CreatedBy, UpdatedBy, IPAddress, CreatedOnUTC, UpdatedOnUTC, Subdomain) values (@OrganisationName, @Website, @CreatedBy, @UpdatedBy, @IPAddress, @CreatedOnUTC, @UpdatedOnUTC, @Subdomain)");
            db.AddInParameter(cmd, "OrganisationName", DbType.String, company.OrganisationName);
            db.AddInParameter(cmd, "Subdomain", DbType.String, company.Subdomain);
            db.AddInParameter(cmd, "Website", DbType.String, company.Website);
            db.AddInParameter(cmd, "CreatedBy", DbType.String, company.CreatedBy);
            db.AddInParameter(cmd, "UpdatedBy", DbType.String, company.UpdatedBy);
            db.AddInParameter(cmd, "IPAddress", DbType.String, company.IPAddress);
            db.AddInParameter(cmd, "CreatedOnUTC", DbType.DateTime, company.CreatedOnUTC);
            db.AddInParameter(cmd, "UpdatedOnUTC", DbType.DateTime, company.UpdatedOnUTC);
            //company.Id = int.Parse(db.ExecuteScalar(cmd).ToString());
            db.ExecuteNonQuery(cmd);
        }


        //public static void UpdateCompany(Company company)
        //{
        //    Database db = ConnectionFactory.CreateDataBase();
        //    DbCommand cmd = db.GetSqlStringCommand($"Update Company set OrganisationName=@OrganisationName, Website=@Website, CreatedBy=@CreatedBy,  UpdatedBy=@UpdatedBy, IPAddress=@IPAddress, CreatedOnUTC=@CreatedOnUTC, UpdatedOnUTC=@UpdatedOnUTC, Subdomain=@Subdomain where Id=@Id");
        //    db.AddInParameter(cmd, "Id", DbType.Int32, company.Id);
        //    db.AddInParameter(cmd, "OrganisationName", DbType.String, company.OrganisationName);
        //    db.AddInParameter(cmd, "Subdomain", DbType.String, company.Subdomain);
        //    db.AddInParameter(cmd, "Website", DbType.String, company.Website);
        //    db.AddInParameter(cmd, "CreatedBy", DbType.String, company.CreatedBy);
        //    db.AddInParameter(cmd, "UpdatedBy", DbType.String, company.UpdatedBy);
        //    db.AddInParameter(cmd, "IPAddress", DbType.String, company.IPAddress);
        //    db.AddInParameter(cmd, "CreatedOnUTC", DbType.DateTime, company.CreatedOnUTC);
        //    db.AddInParameter(cmd, "UpdatedOnUTC", DbType.DateTime, company.UpdatedOnUTC);
        //    db.ExecuteNonQuery(cmd);
        //}


        //public static void DeleteCompany(int id)
        //{
        //    Database db = ConnectionFactory.CreateDataBase();
        //    DbCommand cmd = db.GetSqlStringCommand($"Delete FROM Company where Id = @Id");
        //    db.AddInParameter(cmd, "Id", DbType.Int32, id);
        //    db.ExecuteNonQuery(cmd);
        //}

        /// <summary>
        /// This method fetch all company details from Company table
        /// </summary>
        /// <returns></returns>
        public static List<Company> GetAllCompanies()
        {
            List<Company> companyList = new List<Company>();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * FROM Company");
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                Company company = new Company(reader);
                companyList.Add(company);

            }

            return companyList;

        }
        public static Company GetCompanyById(int Id)
        {
            Company company = new Company();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * From Company where Id=@ID");
            db.AddInParameter(cmd, "ID", DbType.Int32, Id);
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                company = new Company(reader);

            }
            return company;
        }
        public static Company GetByCompanySubdomain(string domain)
        {
            Company company = new Company();
            Database db = ConnectionFactory.CreateDataBase();
            DbCommand cmd = db.GetSqlStringCommand($"Select * From Company where Subdomain=@Subdomain");
            db.AddInParameter(cmd, "Subdomain", DbType.String, domain);
            IDataReader reader = db.ExecuteReader(cmd);
            while (reader.Read())
            {
                company = new Company(reader);

            }
            return company;
        }
    }
}
