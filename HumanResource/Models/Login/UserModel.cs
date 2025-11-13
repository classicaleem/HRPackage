namespace HumanResource.Models.Login
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }        
        public int CompanyID { get; set; }
        public string RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }


}
