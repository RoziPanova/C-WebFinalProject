namespace AspNetCoreArchTemplate.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    [Comment("CustomOrders in the system")]
    public class CustomOrder
    {
        [Comment("CustomOrder identifier")]
        public Guid Id { get; set; }

        [Comment("Customer name")]
        public string UserName { get; set; } = null!;

        [Comment("Customer phone number")]
        //[RegularExpression(PhoneNumberFormat, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Customer address")]
        public string Address { get; set; } = null!;

        [Comment("CustomOrder details")]
        public string Details { get; set; } = null!;

        [Comment("Date CustomOrder is needed on")]
        public DateOnly RequestedDate { get; set; }
    }
}
