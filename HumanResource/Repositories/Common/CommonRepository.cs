using Azure;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using HumanResource.Data;
using HumanResource.Interface;
using HumanResource.Interface.Common;
using HumanResource.Models;
using HumanResource.Models.Common;
using HumanResource.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResource.Repositories.Common
{
    public class CommonRepository : ICommonRepository
    {
        private readonly IConfiguration _config;
        private readonly DapperDBContext context;
       
        public CommonRepository(IConfiguration config, DapperDBContext context)
        {
            _config = config;
            this.context = context;
        }

        //-------------------Start Category--------------------------------------
        public async Task<int> CreateCategory(Category category)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Category (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var categoryId = await connection.ExecuteScalarAsync<int>(query, category);

            return categoryId;
        }

       
        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            //var connectionString = _config.GetConnectionString("DefaultConnection");

            //using IDbConnection connection = new SqlConnection(connectionString);


            //if (connection is SqlConnection sqlConnection)
            //{
            //    await sqlConnection.OpenAsync();
            //}
            ////await connection.OpenAsync();

            //var query = "SELECT * FROM Mst_Category";
            //var categories = await connection.QueryAsync<Category>(query);

            //return categories;


            string query = "Select * From Mst_Category";
            using(var connection=this.context.CreateConnection())
            {
                var emplist= await connection.QueryAsync<Category>(query);
                return emplist.ToList();
            }
        }

        public Task<Category> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        //-------------------End Category--------------------------------------

        //-------------------Start Department--------------------------------------
        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            string query = "Select * From Mst_Department";
            using (var connection = this.context.CreateConnection())
            {
                var deptlist = await connection.QueryAsync<Department>(query);
                return (IEnumerable<Department>)deptlist.ToList();
            }
        }

        public async Task<int> CreateDepartment(Department department)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Department (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var departmentId = await connection.ExecuteScalarAsync<int>(query, department);

            return departmentId;
        }

        //-------------------End Department--------------------------------------

        //-------------------Start Designation--------------------------------------

        public async Task<IEnumerable<Designation>> GetAllDesignation()
        {
            string query = "Select * From Mst_Designation";
            using (var connection = this.context.CreateConnection())
            {
                var deslist = await connection.QueryAsync<Designation>(query);
                return (IEnumerable<Designation>)deslist.ToList();
            }
        }

        public async Task<int> CreateDesignation(Designation designation)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Designation (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var designationId = await connection.ExecuteScalarAsync<int>(query, designation);

            return designationId;
        }
        //-------------------End Designation----------------------------------

        //-------------------Start Skill--------------------------------------
        public async Task<IEnumerable<Skill>> GetAllSkill()
        {
            string query = "Select * From Mst_Skill";
            using (var connection = this.context.CreateConnection())
            {
                var skilllist = await connection.QueryAsync<Skill>(query);
                return (IEnumerable<Skill>)skilllist.ToList();
            }
        }

        public async Task<int> CreateSkill(Skill skill)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Skill (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var skillId = await connection.ExecuteScalarAsync<int>(query, skill);

            return skillId;
        }

        //----------------------End Skill---------------------------------------



        //----------------------Start Section---------------------------------------
        public async Task<IEnumerable<Section>> GetAllSection()
        {
            string query = "Select * From Mst_Section";
            using (var connection = this.context.CreateConnection())
            {
                var sectionlist = await connection.QueryAsync<Section>(query);
                return (IEnumerable<Section>)sectionlist.ToList();
            }
        }

        public async Task<int> CreateSection(Section section)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Section (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var sectionID = await connection.ExecuteScalarAsync<int>(query, section);

            return sectionID;
        }

        //----------------------End Section---------------------------------------

        //----------------------Start LIne---------------------------------------

        public async Task<IEnumerable<Line>> GetAllLine()
        {
            string query = "Select * From Mst_Line";
            using (var connection = this.context.CreateConnection())
            {
                var linelist = await connection.QueryAsync<Line>(query);
                return (IEnumerable<Line>)linelist.ToList();
            }
        }

        public async Task<int> CreateLine(Line line)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Line (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var lineID = await connection.ExecuteScalarAsync<int>(query, line);

            return lineID;
        }

        //----------------------End Line---------------------------------------

        //----------------------Start Company----------------------------------

        public async Task<IEnumerable<Company>> GetAllCompany()
        {
            string query = "Select * From Mst_Company";
            using (var connection = this.context.CreateConnection())
            {
                var companylist = await connection.QueryAsync<Company>(query);
                return (IEnumerable<Company>)companylist.ToList();
            }
        }

        public async Task<int> CreateCompany(Company company)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Company (CompanyName,CNameInTamil,ShortName,GroupCode,SubGroupCode,Address,AddressInTamil,City,CityInTamil,State,StateInTamil,PinCode,Mobile,Phone,Email,PFESTCode,ESICode) VALUES (@CompanyName,@CNameInTamil,@ShortName,@GroupCode,@SubGroupCode,@Address,@AddressInTamil,@City,@CityInTamil,@State,@StateInTamil,@PinCode,@Mobile,@Phone,@Email,@PFESTCode,@ESICode); SELECT CAST(SCOPE_IDENTITY() as int)";
            var companyID = await connection.ExecuteScalarAsync<int>(query, company);

            return companyID;
        }

        //----------------------End Company---------------------------------------

        //----------------------Start Holiday---------------------------------------

        public async Task<IEnumerable<Holiday>> GetAllHoliday()
        {
            string query = "Select * From Mst_Holidays";
            using (var connection = this.context.CreateConnection())
            {
                var holidaylist = await connection.QueryAsync<Holiday>(query);
                return holidaylist.ToList();
            }
        }

        public async Task<int> CreateHoliday(Holiday holiday)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Holidays (HolidayName,HolidayType,HolidayDate) VALUES (@HolidayName,@HolidayType,@HolidayDate); SELECT CAST(SCOPE_IDENTITY() as int)";
            var holidayId = await connection.ExecuteScalarAsync<int>(query, holiday);

            return holidayId;
        }

        //----------------------End Holiday---------------------------------------

        //----------------------Start Shift---------------------------------------
        public async Task<IEnumerable<Shift>> GetAllShift()
        {
            string query = "Select * From Mst_Shift";
            using (var connection = this.context.CreateConnection())
            {
                var shiftlist = await connection.QueryAsync<Shift>(query);
                return shiftlist.ToList();
            }
        }

        public async Task<int> CreateShift(Shift shift)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Shift (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var shiftId = await connection.ExecuteScalarAsync<int>(query, shift);

            return shiftId;
        }

        //----------------------End Shift---------------------------------------

        //----------------------Start Leave---------------------------------------

        public async Task<IEnumerable<Leave>> GetAllLeave()
        {
            string query = "Select * From Mst_Leave";
            using (var connection = this.context.CreateConnection())
            {
                var leavelist = await connection.QueryAsync<Leave>(query);
                return leavelist.ToList();
            }
        }

        public async Task<int> CreateLeave(Leave leave)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Leave (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)";
            var leaveId = await connection.ExecuteScalarAsync<int>(query, leave);

            return leaveId;
        }

        //----------------------End Leave---------------------------------------

        //----------------------Start Bank---------------------------------------

        public async Task<IEnumerable<Bank>> GetAllBank()
        {
            string query = "Select * From Mst_Bank";
            using (var connection = this.context.CreateConnection())
            {
                var banklist = await connection.QueryAsync<Bank>(query);
                return banklist.ToList();
            }
        }

        public async Task<int> CreateBank(Bank bank)
        {
            int bankId=0;
            try
            {
           

            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Mst_Bank (BankName,Branch,IFSC_Code,Address) VALUES (@BankName,@Branch,@IFSC_Code,@Address); SELECT CAST(SCOPE_IDENTITY() as int)";
             bankId = await connection.ExecuteScalarAsync<int>(query, bank);
            }
            catch (Exception e)
            {
                
                Widget.LogFileWrite("EmployeeIndex-DataTable", e.Message.ToString());
                
            }
            return bankId;
        }

        //----------------------End Bank---------------------------------------
    }
}
