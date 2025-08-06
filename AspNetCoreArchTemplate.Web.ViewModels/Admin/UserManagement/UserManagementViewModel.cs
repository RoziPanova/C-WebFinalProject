namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.UserManagement
{
    public class UserManagementViewModel
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
