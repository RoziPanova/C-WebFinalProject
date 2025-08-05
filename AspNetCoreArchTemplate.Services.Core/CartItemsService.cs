namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Cart;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;

    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class CartItemsService : ICartItemsService
    {
        public readonly ICartItemsRepository cartItemsRepository;
        public readonly ICartRepository cartRepository;
        public CartItemsService(ICartItemsRepository cartItemsRepository, ICartRepository cartRepository)
        {
            this.cartItemsRepository = cartItemsRepository;
            this.cartRepository = cartRepository;
        }



        public async Task<IEnumerable<CartIndexViewModel>> GetAllCartItemsAysnc(string userId)
        {
            IEnumerable<CartIndexViewModel> carItems = await this.cartItemsRepository
                .GetAllAttached()
                .Include(ci => ci.Product)
                .AsNoTracking()
                .Where(ci => ci.Cart.UserId.ToLower() == userId.ToLower())
                .Select(ci => new CartIndexViewModel()
                {
                    ProductId = ci.ProductId.ToString(),
                    ProductName = ci.Product.Name,
                    ProductType = ci.Product.ProductType,
                    ProductImageUrl = ci.Product.ImageUrl ?? $"{NoImageUrl}",
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToArrayAsync();

            return carItems;
        }
        public async Task<bool> AddProductToCartAsync(string? productId, string userId)
        {
            bool result = false;
            if (productId != null && userId != null)
            {
                bool isProductIdValid = Guid.TryParse(productId, out Guid productGuid);
                if (isProductIdValid)
                {
                    var cart = await cartRepository
                                .GetAllAttached()
                                .Where(c => c.UserId == userId && !c.IsCheckedOut)
                                .Include(c => c.Items)
                                .SingleOrDefaultAsync();
                    if (cart == null)
                    {
                        cart = new Cart()
                        {
                            UserId = userId,
                            Items = []
                        };
                        await cartRepository.AddAsync(cart);
                        await cartRepository.SaveChangesAsync();
                    }

                    // Check if the item already exists in the cart
                    var existingItem = cart.Items
                        .FirstOrDefault(ci => ci.ProductId == productGuid);

                    if (existingItem != null)
                    {
                        existingItem.Quantity++;
                        result = true;
                    }
                    else
                    {
                        CartItem cartItem = new CartItem()
                        {
                            CartId = cart.Id,
                            ProductId = productGuid,
                            Quantity = 1,
                        };

                        cart.Items.Add(cartItem);
                        result = true;
                    }
                    await cartRepository.SaveChangesAsync();
                }
            }

            return result;
        }

        public async Task<bool> RemoveProductFromCartAsync(string? productId, string userId)
        {
            bool result = false;
            if (!Guid.TryParse(productId, out Guid productGuid)
                || string.IsNullOrEmpty(userId))
            {
                return result;
            }

            var cart = await cartRepository
                .GetAllAttached()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId.ToLower() == userId.ToLower());

            if (cart == null)
            {
                return result;
            }

            // Find the item to remove
            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productGuid);

            if (itemToRemove == null)
            {
                return result;
            }
            cart.Items.Remove(itemToRemove);
            await cartItemsRepository.HardDeleteAsync(itemToRemove);
            result = true;
            await cartItemsRepository.SaveChangesAsync();

            return result;
        }
        public async Task<bool> CheckoutAsync(string userId)
        {
            bool result = false;
            if (string.IsNullOrEmpty(userId))
            {
                return result;
            }
            var cart = await cartRepository
                .GetAllAttached()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId.ToLower() == userId.ToLower() && !c.IsCheckedOut);

            if (cart == null)
            {
                return result;
            }

            cart.IsCheckedOut = true;
            result = true;

            await cartRepository.SaveChangesAsync();

            return result;
        }

        public async Task IncreaseQuantityAsync(string? productId, string userId)
        {
            if (!Guid.TryParse(productId, out Guid productGuid)) return;

            var cart = await cartRepository
                .GetAllAttached()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId.ToLower() == userId.ToLower() && !c.IsCheckedOut);

            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productGuid);
            if (item != null)
            {
                item.Quantity++;
                await cartItemsRepository.SaveChangesAsync();
            }
        }

        public async Task DecreaseQuantityAsync(string? productId, string userId)
        {
            if (!Guid.TryParse(productId, out Guid productGuid)) return;

            var cart = await cartRepository
                .GetAllAttached()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId.ToLower() == userId.ToLower() && !c.IsCheckedOut);

            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productGuid);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    cart?.Items.Remove(item); // remove if 0 or less
                    await cartItemsRepository.HardDeleteAsync(item);
                }
                await cartItemsRepository.SaveChangesAsync();
            }
        }
    }
}

