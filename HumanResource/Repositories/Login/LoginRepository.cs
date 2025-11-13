using Dapper;
using HumanResource.Interface.Login;
using HumanResource.Models;
using HumanResource.Models.Login;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HumanResource.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _config;

        public LoginRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<UserModel> GetEmployeeById(UserModel _user)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            using IDbConnection connection = new SqlConnection(connectionString);

            if (connection is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }

            var query = "SELECT * FROM Mst_User WHERE UserName = @Id and PasswordHash=@Password";
            _user = await connection.QuerySingleOrDefaultAsync<UserModel>(query, new { Id = _user.UserName,Password=_user.Password });

            return _user;
        }
    }
}
