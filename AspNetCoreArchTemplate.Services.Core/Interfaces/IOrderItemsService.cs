namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.OrderItems;

    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItemsIndexViewModel>> GetAllOrderedItemsAync();
        Task AddItemToCart(string? id);
    }
}
