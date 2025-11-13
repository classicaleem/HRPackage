using Dapper;
using HumanResource.Models;
using Microsoft.Data.SqlClient;

namespace HumanResource.Interface
{
    public interface ILookupRepository
    {
        IEnumerable<LookupItem> GetGrades();
        IEnumerable<LookupItem> GetQualifications();
        IEnumerable<LookupItem> GetDepartments();
        IEnumerable<LookupItem> GetCategories();
        IEnumerable<LookupItem> GetIFSC();
        IEnumerable<LookupItem> GetDesignations();
        IEnumerable<LookupItem> GetRoles();
        IEnumerable<LookupItem> GetSection(int ID);
        IEnumerable<LookupItem> GetBank(int ID);
    }

    public class LookupRepository : ILookupRepository
    {
        private readonly IConfiguration _configuration;

        public LookupRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IEnumerable<LookupItem> LoadLookupData(string query)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            return connection.Query<LookupItem>(query);
        }
        //This connection for parameterpass
        private IEnumerable<LookupItem> LoadLookupDataForParameter(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            return connection.Query<LookupItem>(query, parameters);
        }

        public IEnumerable<LookupItem> GetGrades()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LookupItem> GetQualifications()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LookupItem> GetDepartments()
        {
            string query = "SELECT Id, Name FROM Mst_Department";
            return LoadLookupData(query);
        }

        public IEnumerable<LookupItem> GetCategories()
        {
            string query = "SELECT Id, Name FROM Mst_Category";
            return LoadLookupData(query);
        }

        public IEnumerable<LookupItem> GetDesignations()
        {
            string query = "SELECT Id, Name FROM Mst_Designation";
            return LoadLookupData(query);
        }

        public IEnumerable<LookupItem> GetRoles()
        {
            throw new NotImplementedException();
        }

     

        //public IEnumerable<LookupItem> GetSection(int ID)
        //{            
        //    string query = $"SELECT Id, Name FROM Mst_Section WHERE DeptID = {ID}";
        //    return LoadLookupData(query);
        //}

        public IEnumerable<LookupItem> GetSection(int DeptID)
        {
            string query = "SELECT Id, Name FROM Mst_Section WHERE DeptID = @DeptID";
            var parameters = new { DeptID = DeptID };
            return LoadLookupDataForParameter(query, parameters);
        }

        public IEnumerable<LookupItem> GetIFSC()
        {

            string query = "select IFSC_Code as Name,ID  from Mst_Bank where IsDeleted=0";           
            return LoadLookupDataForParameter(query);
        }

        public IEnumerable<LookupItem> GetBank (int ID)
        {
            string query = "select +'Bank: '+BankName+' Branch:   '+Branch+' Bank Address:  '+Address as Name ,ID from Mst_Bank where IsDeleted=0 and ID = @ID";
            var parameters = new { ID = ID };
            return LoadLookupDataForParameter(query, parameters);
            
        }

        // Implement the other methods similarly...
    }

}
