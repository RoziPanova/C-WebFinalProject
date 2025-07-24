namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("CustomOrders in the system")]
    public class CustomOrder
    {
        [Comment("CustomOrder identifier")]
        public Guid Id { get; set; }

        [Comment("Date CustomOrder is needed on")]
        public DateTime RequestedDate { get; set; }

        [Comment("CustomOrder details")]
        public string Details { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        = new HashSet<OrderItem>();
    }
}
