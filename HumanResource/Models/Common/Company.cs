namespace HumanResource.Models.Common
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CNameInTamil { get; set; }
        public string? ShortName { get; set; }
        public int GroupCode { get; set; }
        public int SubGroupCode { get; set; }
        public string? Address { get; set; }
        public string? AddressInTamil { get; set; }
        public string? City { get; set; }
        public string? CityInTamil { get; set; }
        public string? State { get; set; }
        public string? StateInTamil { get; set; }
        public int Pincode { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int PFESTCode { get; set; }
        public string? ESICode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
