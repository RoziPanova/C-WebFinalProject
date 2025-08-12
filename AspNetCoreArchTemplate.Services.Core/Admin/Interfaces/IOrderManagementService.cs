namespace AspNetCoreArchTemplate.Services.Core.Admin.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement;

    public interface IOrderManagementService
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
        Task<IEnumerable<CustomOrderViewModel>> GetAllCustomOrdersAsync();
        Task<bool> DeleteOrderAsync(string id);
        Task<bool> DeleteCustomOrderAsync(string id);
        Task<OrderDetailsViewModel?> GetOrderDetailsAsync(string id);
        Task<CustomOrderViewModel?> GetCustomOrderDetailsAsync(string id);
    }
}
