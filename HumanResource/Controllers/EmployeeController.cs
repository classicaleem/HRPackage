using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using HumanResource.Interface;
using HumanResource.Models;
using HumanResource.Models.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HumanResource.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILookupRepository _lookupRepository;
        



        public EmployeeController(IConfiguration config, IEmployeeRepository employeeRepository, ILookupRepository lookupRepository)
        {
            _config = config;
            _employeeRepository = employeeRepository;
            _lookupRepository = lookupRepository;
            

        }


        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{


        //    var employees = await _employeeRepository.GetAllEmployees();
        //    return View(employees);
        //}

        [HttpGet]
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<JsonResult> GetData(int draw, int start, int length)
        {
            
            string searchValue = Request.Form["search[value]"];
            
            int totalRecords = await _employeeRepository.GetTotalEmployeeCountAsync(searchValue);
            List<EmployeeModel> products = await _employeeRepository.GetAllEmployeeAsync(searchValue, start, length);

            var result = new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = products
            };
            
            return Json(result);
        }

        [HttpGet]
        public IActionResult Create()
        {            

            var viewModel = new EmployeeModel            {
                
                lstDepartment = _lookupRepository.GetDepartments().Select(g => new SelectListItem(g.Name, g.Id.ToString())),
                lstCategory = _lookupRepository.GetCategories().Select(g => new SelectListItem(g.Name, g.Id.ToString())),
                lstIFSC = _lookupRepository.GetIFSC().Select(g => new SelectListItem(g.Name, g.Id.ToString())),
                //lstDesignation = _lookupRepository.GetDesignations().Select(g => new SelectListItem(g.Name, g.Id.ToString())),
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetSection(int sectionId)
        {
            var Section = _lookupRepository.GetSection(sectionId);
            var SectionItems = Section.Select(d => new SelectListItem(d.Name, d.Id.ToString()));
            return Json(SectionItems);
        }

        [HttpGet]
        public IActionResult GetBank(int IFSCID)
        {
            var Section = _lookupRepository.GetBank(IFSCID);
            var SectionItems = Section.Select(d => new SelectListItem(d.Name, d.Id.ToString())).FirstOrDefault();
            return Json(SectionItems);
        }



        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle empty model
                ModelState.AddModelError("", "Please provide employee details.");

                return View(model);
            }
            var ProfilePhoto = Request.Form.Files["ProfilePhoto"];

            if (ProfilePhoto != null && ProfilePhoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ProfilePhoto.CopyTo(memoryStream);
                    model.ProfilePhoto = memoryStream.ToArray();
                }
            }
            // Handle image upload
            //string photoPath = await SaveFileToOtherServer(ProfilePhoto, _config);

            await _employeeRepository.CreateEmployee(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            if (model == null)
            {
                // Handle empty model
                ModelState.AddModelError("", "Please provide employee details.");
                return View(model);
            }

            // Handle image upload
            //if (model.ImageFile != null && model.ImageFile.Length > 0)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await model.ImageFile.CopyToAsync(memoryStream);
            //        model.ImageData = memoryStream.ToArray();
            //    }
            //}

            await _employeeRepository.UpdateEmployee(model);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete()
        //{
        //    var employee = await _employeeRepository.GetEmployeeById(id);


        //    return View(employee);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
            return RedirectToAction("Index");
        }


        public ActionResult ExportToExcel()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Employees"; // Adjust the query as needed to fetch all data

                var data = connection.Query<EmployeeModel>(query).ToList();

                // Create an Excel workbook and worksheet
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Data");

                // Set header
                var headerCell = worksheet.Cell(1, 1).InsertData(new List<string> { "Employee List" });
                headerCell.Merge(); // Merge the header cell across all columns
                headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Align the text in the merged cell

                // Set column headers
                worksheet.Cell(2, 1).Value = "Name";
                worksheet.Cell(2, 2).Value = "Age";
                worksheet.Cell(2, 3).Value = "Blood Group";
                worksheet.Cell(2, 4).Value = "Father Name";
                worksheet.Cell(2, 5).Value = "ESI Number";
                worksheet.Cell(2, 6).Value = "PF Number";
                worksheet.Cell(2, 7).Value = "Gender";                
                worksheet.Cell(2, 8).Value = "Mobile";
                worksheet.Cell(2, 9).Value = "DOJ";
                worksheet.Cell(2, 10).Value = "UAN";
                worksheet.Cell(2, 11).Value = "AadharCard No";
                worksheet.Cell(2, 12).Value = "Religion";
                worksheet.Cell(2, 13).Value = "Address";
                // Add more headers as needed

                // Populate data
                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = data[i].Name;
                    worksheet.Cell(i + 3, 2).Value = data[i].Age;
                    worksheet.Cell(i + 3, 3).Value = data[i].BloodGroup;
                    worksheet.Cell(i + 3, 4).Value = data[i].FatherName;
                    worksheet.Cell(i + 3, 5).Value = data[i].ESINo;
                    worksheet.Cell(i + 3, 6).Value = data[i].PFNo;
                    worksheet.Cell(i + 3, 7).Value = data[i].Gender;                    
                    worksheet.Cell(i + 3, 8).Value = data[i].MobileNo;
                    worksheet.Cell(i + 3, 9).Value = data[i].DateOfJoining;
                    worksheet.Cell(i + 3, 10).Value = data[i].UANNo;
                    worksheet.Cell(i + 3, 11).Value = data[i].AadharCardNo;
                    worksheet.Cell(i + 3, 12).Value = data[i].Religion;
                    worksheet.Cell(i + 3, 13).Value = data[i].Address;
                    
                    // Add more data cells as needed
                }

                // Set content type and headers for Excel file download
                var ms = new MemoryStream();
                workbook.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);

                // Auto-align columns based on content size
                worksheet.Columns().AdjustToContents();

                var fileDownloadName = "employee_list.xlsx";

                // Use FileContentResult to return the Excel file
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName);
            }
        }

        public ActionResult ExportToExcel2()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Employees"; // Adjust the query as needed to fetch all data

                var data = connection.Query<EmployeeModel>(query).ToList();

                // Create an Excel workbook and worksheet
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Data");

                // Set column headers
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Age";
                worksheet.Cell(1, 3).Value = "Blood Group";
                // Add more headers as needed

                // Populate data
                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = data[i].Name;
                    worksheet.Cell(i + 2, 2).Value = data[i].Age;
                    worksheet.Cell(i + 3, 3).Value = data[i].BloodGroup;
                    // Add more data cells as needed
                }

                // Set content type and headers for Excel file download
                var ms = new MemoryStream();
                workbook.SaveAs(ms);
                ms.Seek(0, SeekOrigin.Begin);

                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeList.xlsx");
            }
        }



        private async Task<string> SaveFileToOtherServer(IFormFile file, IConfiguration configuration)
        {
            // Read settings from appsettings.json
            string serverIpAddress = configuration["OtherServerSettings:ServerIpAddress"];
            string serverUsername = configuration["OtherServerSettings:ServerUsername"];
            string serverPassword = configuration["OtherServerSettings:ServerPassword"];
            string serverPort = configuration["OtherServerSettings:ServerPort"];
            string serverFolderPath = configuration["OtherServerSettings:ServerFolderPath"];

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Ensure the port is a valid integer
            int.TryParse(serverPort, out int port);

            // Construct the FTP URL with the desired path
            //string ftpUrl = $"ftp://{serverIpAddress}/{serverFolderPath.Replace('\\', '/')}/{fileName}";
            //string ftpUrl = $"ftp:\\{serverIpAddress}\\{serverFolderPath}\\{fileName}";
            string escapedFolderPath = serverFolderPath.Replace("\\", "\\");
            //string ftpUrl = $"ftp://{serverIpAddress}/{escapedFolderPath}/{fileName}";
            string ftpUrl = $"ftp:\\{serverIpAddress}\\{escapedFolderPath}\\{fileName}";



            // Upload the file to the FTP server
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(serverUsername, serverPassword);

                // Read the file content into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    // Upload the byte array to the remote server
                    await client.UploadDataTaskAsync(new Uri(ftpUrl), "STOR", fileBytes);
                }
            }


            //string FolderPath = "C:\\TestUpload\\FileUpload\\";
            //string ftpUrl = FolderPath;
            //var uploaded_files1 = Request.Form.Files;
            //int iCounter = 0;
            //string sFiles_uploaded = "";
            //foreach (var uploaded_file in uploaded_files1)
            //{
            //    iCounter++;
            //    sFiles_uploaded += "," + uploaded_file.FileName;
            //    string uploaded_Filename = uploaded_file.FileName;
            //    string new_Filename_on_Server = ftpUrl + "\\" + uploaded_Filename;
            //    using (FileStream stream = new FileStream(new_Filename_on_Server, FileMode.Create))
            //    {
            //        await uploaded_file.CopyToAsync(stream);
            //    }
            //}



            return ftpUrl;
        }


        [HttpGet]
        public async Task<JsonResult> GetNextEmployeeId(int CategoryID)
        {
            string prefix = "";

            UserModel logonModel = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("UserDetails"));

            string companyName = await _employeeRepository.GetCompanyshortname(logonModel.CompanyID);
            if (CategoryID ==1)
            {
                prefix = "S";

            }
            else if (CategoryID ==2)
            {
                prefix = "W";

            }
            else if (CategoryID == 3)
            {
                prefix = "T";

            }
            else if(CategoryID == 4)
            {
                prefix = "G";
            }
            else if (CategoryID == 5)
            {
                prefix = "D";

            }
            else if (CategoryID == 6) { prefix = "4"; }
            else if (CategoryID == 7) { prefix = "5"; } 
            else { }

            prefix = companyName + prefix+"-";


            int maxId = await _employeeRepository.GetMaxEmployeeIdByCategory(CategoryID);
            int maxPunchID = await _employeeRepository.GetMaxEmployeePunchIdByCategory();

            string newEmployeeId = GenerateNextEmployeeId(maxId);
            newEmployeeId = prefix+ newEmployeeId ;
            
            var result = new
            {
                EmployeeCode = newEmployeeId,
                Empid = maxPunchID,
            };

            
            return Json(result);
        }


        private string GenerateNextEmployeeId(int maxId)
        {
            

            // Extract the numeric part from the ID
            string maxIdNumericPart = (maxId > 0) ? maxId.ToString("D4") : "0000";

            // Convert the numeric part to an integer
            int nextNumericPart = int.Parse(maxIdNumericPart) + 1;

            // Format the new numeric part as a string with leading zeros
            string newNumericPart = nextNumericPart.ToString("D4");

            // Combine the prefix and the new numeric part to create the new employee ID
            string newEmployeeId = newNumericPart;

            return newEmployeeId;
        }


    }
}
