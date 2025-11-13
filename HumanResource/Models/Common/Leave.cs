namespace HumanResource.Models.Common
{
    public class Leave
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
