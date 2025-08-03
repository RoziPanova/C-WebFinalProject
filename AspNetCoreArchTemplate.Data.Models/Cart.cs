using Microsoft.AspNetCore.Identity;

namespace AspNetCoreArchTemplate.Data.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CartItem> Items { get; set; }
            = new HashSet<CartItem>();
        public bool IsCheckedOut { get; set; } = false;
    }
}