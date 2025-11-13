using HumanResource.Models;

namespace HumanResource.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> GetAllEmployees();
        
        Task<List<EmployeeModel>> GetAllEmployeeAsync(string searchValue, int start, int length);
        //Task<List<EmployeeModel>> GetAllEmployeeAsync(string searchValue, int start, int length, string sortColumn, string sortDirection);
        Task<int> GetTotalEmployeeCountAsync(string searchValue);
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<int> CreateEmployee(EmployeeModel employee);
        Task<bool> UpdateEmployee(EmployeeModel employee);
        Task<bool> DeleteEmployee(int id);
        Task<int> GetMaxEmployeeIdByCategory(int category);
        Task<int> GetMaxEmployeePunchIdByCategory();
        Task<string> GetCompanyshortname(int Companycode);
    }
}
