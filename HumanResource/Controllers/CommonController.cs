using HumanResource.Interface.Common;
using HumanResource.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Controllers
{
    public class CommonController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ICommonRepository _CommonRepository;

        public CommonController(IConfiguration config, ICommonRepository CommonRepository)
        {
            _config = config;
            _CommonRepository = CommonRepository;

        }

        /// Category
        //----------------Start Category -------------------------
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(Category _category)
        {
            await _CommonRepository.CreateCategory(_category);

            if (_category == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }


        public async Task<IActionResult> CategoryIndex()
        {
            var categories = await _CommonRepository.GetAllCategory();
            return View(categories);
        }

        //----------------End Category -------------------------

       ///Department
       //------------------Start Department---------------------
        [HttpGet]
        public IActionResult DepartmentCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentCreate(Department _department)
        {
            await _CommonRepository.CreateDepartment(_department);

            if (_department == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }

        public async Task<IActionResult> DepartmentIndex()
        {
            var departments = await _CommonRepository.GetAllDepartment();
            return View(departments);
        }
        //------------------End Department---------------------

       ///Designation
       //------------------Start Designation---------------------
        [HttpGet]
        public IActionResult DesignationCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DesignationCreate(Designation _designation)
        {
            await _CommonRepository.CreateDesignation(_designation);

            if (_designation == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }

        public async Task<IActionResult> DesignationIndex()
        {
            var designation = await _CommonRepository.GetAllDesignation();
            return View(designation);
        }
        //------------------End Designation---------------------

        ///Skill
       //------------------Start Skill---------------------
        [HttpGet]
        public IActionResult SkillCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SkillCreate(Skill _skill)
        {
            await _CommonRepository.CreateSkill(_skill);

            if (_skill == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }

        public async Task<IActionResult> SkillIndex()
        {
            var skill = await _CommonRepository.GetAllSkill();
            return View(skill);
        }
        //------------------End Skill---------------------


        ///Section
        //------------------Start Section---------------------
        [HttpGet]
        public IActionResult SectionCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SectionCreate(Section _section)
        {
            await _CommonRepository.CreateSection(_section);

            if (_section == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }

        public async Task<IActionResult> SectionIndex()
        {
            var section = await _CommonRepository.GetAllSection();
            return View(section);
        }
        //------------------End Section---------------------

        ///Section
        //------------------Start Line---------------------
        [HttpGet]
        public IActionResult LineCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LineCreate(Line _line)
        {
            await _CommonRepository.CreateLine(_line);

            if (_line == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }

        public async Task<IActionResult> LineIndex()
        {
            var line = await _CommonRepository.GetAllLine();
            return View(line);
        }
        //------------------End Line---------------------

        ///Company
        //------------------Start Company---------------------
        [HttpGet]
        public IActionResult CompanyCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompanyCreate(Company _company)
        {
            await _CommonRepository.CreateCompany(_company);

            if (_company == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return RedirectToAction("CompanyIndex","Common");
            }
        }

        public async Task<IActionResult> CompanyIndex()
        {
            var company = await _CommonRepository.GetAllCompany();
            return View(company);
        }
        //------------------End Company-------------------------

        ///Holiday
        //------------------Start Holiday---------------------
        [HttpGet]
        public IActionResult HolidayCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HolidayCreate(Holiday _holiday)
        {
            await _CommonRepository.CreateHoliday(_holiday);

            if (_holiday == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }

        public async Task<IActionResult> HolidayIndex()
        {
            var holiday = await _CommonRepository.GetAllHoliday();
            return View(holiday);
        }
        //------------------End Holiday-------------------------

        ///Shift
        //------------------Start Shift---------------------
        [HttpGet]
        public IActionResult ShiftCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ShiftCreate(Shift _shift)
        {
            object value = await _CommonRepository.CreateShift(_shift);

            if (_shift == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }


        public async Task<IActionResult> ShiftIndex()
        {
            var shifts = await _CommonRepository.GetAllShift();
            return View(shifts);
        }
        //------------------End Shift-------------------------

        ///Leave
        //------------------Start Leave---------------------
        [HttpGet]
        public IActionResult LeaveCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LeaveCreate(Leave _leave)
        {
            object value = await _CommonRepository.CreateLeave(_leave);

            if (_leave == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }


        public async Task<IActionResult> LeaveIndex()
        {
            var leaves = await _CommonRepository.GetAllLeave();
            return View(leaves);
        }
        //------------------End Leave-------------------------

        ///Leave
        //------------------Start Bank---------------------
        [HttpGet]
        public IActionResult BankCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BankCreate(Bank _bank)
        {
            object value = await _CommonRepository.CreateBank(_bank);

            if (_bank == null)
            {
                ModelState.AddModelError("", "Username or password incorrect! Please try again.");
                return Json("");
            }
            else
            {
                return Json("");
            }
        }


        public async Task<IActionResult> BankIndex()
        {
            var banks = await _CommonRepository.GetAllBank();
            return View(banks);
        }
        //------------------End Bank-------------------------
    }
}
