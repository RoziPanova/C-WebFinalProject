namespace AspNetCoreArchTemplate.Web.ViewModels.CustomOrder
{
    using System.ComponentModel.DataAnnotations;
    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;
    public class CustomOrderFormInputViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Details about the order are required")]
        public string Details { get; set; } = null!;

        [Required(ErrorMessage = "The request date is required")]
        public string RequestedDate { get; set; } = null!;

    }
}
