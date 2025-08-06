namespace AspNetCoreArchTemplate.Services.Core.Admin
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.UserManagement;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManagementService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IEnumerable<UserManagementViewModel>> GetUserManagementDataAsync(string userId)
        {
            IEnumerable<UserManagementViewModel> users = await this.userManager
               .Users
               .Where(u => u.Id.ToLower() != userId.ToLower())
               .Select(u => new UserManagementViewModel
               {
                   Id = u.Id,
                   Email = u.Email!,
                   Roles = userManager.GetRolesAsync(u)
                       .GetAwaiter()
                       .GetResult()
               })
               .ToArrayAsync();

            return users;
        }

        public async Task<bool> AssignUserToRoleAsync(RoleSelectionInputModel inputModel)
        {
            ApplicationUser? user = await this.userManager
                .FindByIdAsync(inputModel.UserId);

            if (user == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            bool roleExists = await this.roleManager.RoleExistsAsync(inputModel.Role);
            if (!roleExists)
            {
                throw new ArgumentException("Selected role is not a valid role!");
            }

            try
            {
                await this.userManager.AddToRoleAsync(user, inputModel.Role);

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Unexpected error occurred while adding the user to role! Please try again later!",
                    innerException: e);
            }
        }

        public async Task<bool> DeleteUserById(string userId)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(userId))
            {
                var user = await this.userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await this.userManager.DeleteAsync(user);
                    result = true;
                }
                return result;
            }
            return result;
        }
    }
}
