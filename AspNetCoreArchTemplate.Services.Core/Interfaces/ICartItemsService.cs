namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.Cart;

    public interface ICartItemsService
    {
        Task<IEnumerable<CartIndexViewModel>> GetAllCartItemsAsync(string userId);
        Task<bool> AddProductToCartAsync(string? productId, string userId);
        Task<bool> RemoveProductFromCartAsync(string? productId, string userId);
        Task<bool> CheckoutAsync(string userId);
        Task DecreaseQuantityAsync(string? productId, string userId);
        Task IncreaseQuantityAsync(string? productId, string userId);
    }
}
