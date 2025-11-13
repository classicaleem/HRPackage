using HumanResource.Interface;

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HumanResource.Models;
using HumanResource.Utils;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace HumanResource.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _config;

        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
        }


        public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);


            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }
            //await connection.OpenAsync();

            var query = "SELECT * FROM Employees";
            var employees = await connection.QueryAsync<EmployeeModel>(query);

            return employees;



        }

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "SELECT * FROM Employees WHERE Id = @Id";
            var employee = await connection.QuerySingleOrDefaultAsync<EmployeeModel>(query, new { Id = id });

            return employee;
        }

        public async Task<int> CreateEmployee(EmployeeModel employee)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "INSERT INTO Employees (CategoryID,EmployeeCode,EmployeeId,Name,Age,BloodGroup,Image,FatherName,ESINo,PFNo,Gender,DateOfJoining,DateOfLeaving,MobileNo,UANNo,AadharCardNo,Religion,Address,BankCode) VALUES (@Category,@EmployeeCode,@EmployeeId,@Name,@Age,@BloodGroup,@ProfilePhoto,@FatherName,@ESINo,@PFNo,@Gender,@DateOfJoining,@DateOfLeaving,@MobileNo,@UANNo,@AadharCardNo,@Religion,@Address,@BankID); SELECT CAST(SCOPE_IDENTITY() as int)";
            var employeeId = await connection.ExecuteScalarAsync<int>(query, employee);

            return employeeId;
        }

        public async Task<bool> UpdateEmployee(EmployeeModel employee)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "UPDATE Employees SET Name = @Name, Age = @Age, BloodGroup = @BloodGroup WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(query, employee);

            return affectedRows > 0;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "DELETE FROM Employees WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });

            return affectedRows > 0;
        }

        //public async Task<List<EmployeeModel>> GetAllEmployeeAsync(string searchValue, int start, int length, string sortDirection, int sortColumn)
        //{
        //    try
        //    {
        //        var connectionString = _config.GetConnectionString("DefaultConnection");
        //        using (IDbConnection connection = new SqlConnection(connectionString))
        //        {
        //            // Perform the data retrieval query based on the search value, start, and length
        //            string filterQuery = string.IsNullOrWhiteSpace(searchValue)
        //                ? string.Empty
        //                : $"WHERE Name LIKE '%{searchValue}%' OR  ESINo LIKE '%{searchValue}%' OR  Religion LIKE '%{searchValue}%'";

        //            // string orderByColumn = GetOrderByColumn(sortColumn); // Map sortColumn to actual column name
        //            string orderByColumn = "ID";

        //            // Construct the query for data retrieval with sorting
        //            string query = $@"
        //        SELECT * FROM (
        //            SELECT ROW_NUMBER() OVER (ORDER BY {orderByColumn} {sortDirection}) AS RowNum, ID,Name,Age,BloodGroup,FatherName,ESINo,PFNo,Gender,DateOfJoining,DateOfLeaving,MobileNo,UANNo,AadharCardNo,Religion,Address
        //            FROM Employees {filterQuery}
        //        ) AS Paged
        //        WHERE RowNum > {start} AND RowNum <= {start + length}
        //        ORDER BY RowNum";

        //            var employees = await connection.QueryAsync<EmployeeModel>(query);
        //            return employees.ToList();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Widget.LogFileWrite("EmployeeIndex-DataTable", e.Message.ToString());
        //        return new List<EmployeeModel>();
        //    }
        //}

        
        private string GetOrderByColumn(int sortColumn)
        {
            switch (sortColumn)
            {
                case 0: return "ID"; // Map your sort columns to actual column names in your Employees table
                case 1: return "Name";
                case 2: return "Age";
                case 3: return "Religion";
                // Add more cases for other columns as needed
                default: return "ID"; // Default sorting column
            }
        }


        public async Task<List<EmployeeModel>> GetAllEmployeeAsync(string searchValue, int start, int length)
        {
            try
                {
                var connectionString = _config.GetConnectionString("DefaultConnection");
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    // Perform the data retrieval query based on the search value, start, and length
                    string filterQuery = string.IsNullOrWhiteSpace(searchValue)
                        ? string.Empty
                        : $"WHERE Name LIKE '%{searchValue}%' OR  ESINo LIKE '%{searchValue}%'" +
                        $"OR  PFNo LIKE '%{searchValue}%'" +
                        $"OR  FatherName LIKE '%{searchValue}%'" +
                        $"OR  UANNo LIKE '%{searchValue}%'" +
                        $"OR  AadharCardNo LIKE '%{searchValue}%'" +                        
                        $"OR  Religion LIKE '%{searchValue}%'";

                    //SELECT ROW_NUMBER() OVER(ORDER BY Id) AS RowNum, ID, Name, Age, BloodGroup, FatherName, ESINo, PFNo, Gender, DateOfJoining, DateOfLeaving, MobileNo, UANNo, AadharCardNo, Religion, Address
                    string query = $@"
                SELECT TOP 10 * FROM (
                    SELECT ROW_NUMBER() OVER (ORDER BY ID) AS RowNum, ID,Name,Age,BloodGroup,FatherName,ESINo,PFNo,Gender,DateOfJoining,DateOfLeaving,MobileNo,UANNo,AadharCardNo,Religion,Address
                    FROM Employees {filterQuery}
                ) AS Paged
                WHERE RowNum > {start} AND RowNum <= {start + length}
                ORDER BY RowNum";

                    var products = await connection.QueryAsync<EmployeeModel>(query);
                    return products.ToList();
                }
            }
            catch (Exception e)
            {
                Widget.LogFileWrite("EmployeeIndex-DataTable", e.Message.ToString());
                return new List<EmployeeModel>();
            }
        }

        public async Task<int> GetTotalEmployeeCountAsync(string searchValue)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // Perform the count query based on the search value
                string filterQuery = string.IsNullOrWhiteSpace(searchValue)
                    ? string.Empty
                    : $"WHERE Name LIKE '%{searchValue}%'";
                string countQuery = $"SELECT COUNT(*) FROM Employees {filterQuery}";
                int totalRecords = await connection.ExecuteScalarAsync<int>(countQuery);
                return totalRecords;
            }

        }

        public async Task<int> GetMaxEmployeeIdByCategory(int category)
        {
            try
            {
                var connectionString = _config.GetConnectionString("DefaultConnection");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT MAX(CAST(SUBSTRING(EmployeeCode, CHARINDEX('-', EmployeeCode) + 1, LEN(EmployeeCode)) AS INT)) " +
                                 "FROM Employees " +
                                 "WHERE CategoryID = @Category";

                    int? maxId = connection.ExecuteScalar<int?>(sql, new { Category = category });

                    return maxId.HasValue ? maxId.Value : 0;
                }
            }
            catch (Exception e)
            {
                Widget.LogFileWrite("EmployeeIndex-DataTable", e.Message.ToString());
                return  0;
            }

        }

        public async Task<int> GetMaxEmployeePunchIdByCategory()
        {
            try
            {
                var connectionString = _config.GetConnectionString("DefaultConnection");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT ISNULL(MAX(EmployeeID), 0) + 1 AS EmployeeID " +
                                 "FROM Employees ";
                                 //+"WHERE CategoryID = @Category";

                    int? maxId = connection.ExecuteScalar<int?>(sql);

                    return maxId.HasValue ? maxId.Value : 0;
                }
            }
            catch (Exception e)
            {
                Widget.LogFileWrite("EmployeeIndex-DataTable", e.Message.ToString());
                return 0;
            }

        }

        public async Task<string> GetCompanyshortname(int Companycode)
        {
            string totalRecords = "";
            try
            {           
                var connectionString = _config.GetConnectionString("DefaultConnection");
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                
                    // Perform the count query based on the search value
                    string filterQuery = $"WHERE CompanyID = '{Companycode}'";
                    string countQuery = $"SELECT ShortName FROM Mst_Company {filterQuery}";
                    totalRecords = await connection.ExecuteScalarAsync<string>(countQuery);
                    
                }
            }
            catch (Exception e)
            {
                Widget.LogFileWrite("EmployeeIndex-DataTable", e.Message.ToString());
                
            }
            return totalRecords;
        }
    }
}
