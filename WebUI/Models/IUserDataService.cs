namespace WebUI.Models
{
    public interface IUserDataService
    {
        public string CurrentUserName { get; set; }
        public UserHelper CurrentUserData { get; set; }
        public string RolodexServiceURL { get; set; }
    }
}
