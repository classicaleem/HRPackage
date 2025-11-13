using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanResource.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CategoryID { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeId { get; set; }
        public string? FatherName { get; set; }
        public int Age { get; set; }
            
        
        public string? BloodGroup { get; set; }
        public string? ESINo { get; set; }
        public string? PFNo { get; set; }

        public string? Gender { get; set; }       
        
        public string? DateOfJoining { get; set; }
        public string? DateOfLeaving { get; set; }
        public int MobileNo { get; set; }
        public int UANNo { get; set; }
        public int AadharCardNo { get; set; }
        public string? Religion { get; set; }
        public string? Address { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public string? Department { get; set; }
        public string? Category { get; set; }
        public int SelectedSectionId { get; set; }
        //public string? IFSCcode { get; set; }
        //public string? BankAddress { get; set; }
        public string? BankID { get; set; }

        public IEnumerable<SelectListItem>? lstDepartment { get; set; }
        public IEnumerable<SelectListItem>? lstIFSC { get; set; }

        public IEnumerable<SelectListItem>? lstCategory { get; set; }
        public IEnumerable<SelectListItem>? lstDesignation { get; set; }
    }
}
