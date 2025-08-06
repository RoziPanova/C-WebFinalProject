using System.ComponentModel.DataAnnotations;

namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.UserManagement
{
    public class RoleSelectionInputModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
