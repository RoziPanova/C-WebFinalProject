namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserOrder> UserOrders { get; set; }
            = new HashSet<UserOrder>();
    }

}
