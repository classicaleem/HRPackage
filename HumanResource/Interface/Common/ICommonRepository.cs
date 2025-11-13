using HumanResource.Models.Common;

namespace HumanResource.Interface.Common
{
    public interface ICommonRepository
    {
        //Category
        Task<IEnumerable<Category>> GetAllCategory();
        Task<int> CreateCategory(Category category);
        //Task<Category> GetCategoryById(int id);
        //Task<bool> UpdateCategory(Category category);
        //Task<bool> DeleteCategory(int id);

        //Department
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<int> CreateDepartment(Department department);

        //Designation
        Task<IEnumerable<Designation>> GetAllDesignation();
        Task<int> CreateDesignation(Designation designation);

        //Skill
        Task<IEnumerable<Skill>> GetAllSkill();
        Task<int> CreateSkill(Skill skill);

        //Section
        Task<IEnumerable<Section>> GetAllSection();
        Task<int> CreateSection(Section section);

        //Line
        Task<IEnumerable<Line>> GetAllLine();
        Task<int> CreateLine(Line line);

        //Line
        Task<IEnumerable<Company>> GetAllCompany();
        Task<int> CreateCompany(Company company);

        //Holiday
        Task<IEnumerable<Holiday>> GetAllHoliday();
        Task<int> CreateHoliday(Holiday holiday);

        //Shift
        Task<IEnumerable<Shift>> GetAllShift();
        Task<int> CreateShift(Shift shift);

        //Leave
        Task<IEnumerable<Leave>> GetAllLeave();
        Task<int> CreateLeave(Leave leave);

        //Bank
        Task<IEnumerable<Bank>> GetAllBank();
        Task<int> CreateBank(Bank bank);

    }
}
