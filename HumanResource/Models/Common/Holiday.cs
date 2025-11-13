namespace HumanResource.Models.Common
{
    public class Holiday
    {
        public int ID { get; set; }
        public string? HolidayName { get; set; }
        public string? HolidayType { get; set; }
        public DateTime HolidayDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
