namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Order> Orders { get; set; }
            = new HashSet<Order>();
    }
}
