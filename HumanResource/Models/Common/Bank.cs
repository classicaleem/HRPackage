namespace HumanResource.Models.Common
{
    public class Bank
    {
        public int ID { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public string? IFSC_Code { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
