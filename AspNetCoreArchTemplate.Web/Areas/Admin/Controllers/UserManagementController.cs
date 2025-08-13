namespace AspNetCoreArchTemplate.Web.Areas.Admin.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.UserManagement;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    public class UserManagementController : BaseAdminController
    {
        private readonly IUserManagementService userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            this.userManagementService = userManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementViewModel> allUsers = await this.userManagementService
                .GetUserManagementDataAsync(this.GetUserId()!);

            return View(allUsers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRole(RoleSelectionInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isUpdated = await this.userManagementService
                        .AssignUserToRoleAsync(inputModel);
                    if (!isUpdated)
                    {
                        return this.NotFound();
                    }

                    return this.RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;

                    return this.RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return this.RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                bool isDeleted = await this.userManagementService
                    .DeleteUserById(userId);
                if (!isDeleted)
                {
                    return this.NotFound();
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;

                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
