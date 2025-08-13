namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.CustomOrder;

    public interface ICustomOrderService
    {
        Task<bool> AddCustomOrderAsync(CustomOrderFormInputViewModel inputModel, string UserId);
        Task<IEnumerable<CustomOrderListViewModel>> GetUserCustomOrdersAsync(string userId);
        Task<CustomOrderDetailsViewModel?> GetCustomOrderDetailsAsync(string customOrderId);
        Task<bool> UpdateCustomOrderAsync(string customOrderId, CustomOrderFormInputViewModel model);
        Task<CustomOrderFormInputViewModel?> GetCustomOrderForEditAsync(string customOrderId);
        Task<bool> DeleteCustomOrderAsync(string customOrderId);
    }
}
