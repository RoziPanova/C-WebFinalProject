namespace AspNetCoreArchTemplate.Services.Core.Admin.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.UserManagement;

    public interface IUserManagementService
    {
        Task<IEnumerable<UserManagementViewModel>> GetUserManagementDataAsync(string userId);
        Task<bool> AssignUserToRoleAsync(RoleSelectionInputModel inputModel);
        Task<bool> DeleteUserById(string userId);
    }
}
