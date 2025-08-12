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
                    Price = ci.Product.Price,
                    Total = ci.Product.Price * ci.Quantity,
                }).ToArrayAsync();

            return carItems;
        }
        public async Task<bool> AddProductToCartAsync(string? productId, string userId)
        {
            bool isProductAddedToCart = false;
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
                        isProductAddedToCart = true;
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
                        isProductAddedToCart = true;
                    }
                    await cartRepository.SaveChangesAsync();
                }
            }

            return isProductAddedToCart;
        }

        public async Task<bool> RemoveProductFromCartAsync(string? productId, string userId)
        {
            bool isProductRemovedFromCart = false;
            if (!Guid.TryParse(productId, out Guid productGuid)
                || string.IsNullOrEmpty(userId))
            {
                return isProductRemovedFromCart;
            }

            var cart = await cartRepository
                .GetAllAttached()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId.ToLower() == userId.ToLower());

            if (cart == null)
            {
                return isProductRemovedFromCart;
            }

            // Find the item to remove
            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productGuid);

            if (itemToRemove == null)
            {
                return isProductRemovedFromCart;
            }
            cart.Items.Remove(itemToRemove);
            await cartItemsRepository.HardDeleteAsync(itemToRemove);
            isProductRemovedFromCart = true;
            await cartItemsRepository.SaveChangesAsync();

            return isProductRemovedFromCart;
        }
        public async Task<bool> CheckoutAsync(string userId)
        {
            bool isCartCheckedOut = false;
            if (string.IsNullOrEmpty(userId))
            {
                return isCartCheckedOut;
            }
            var cart = await cartRepository
                .GetAllAttached()
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId.ToLower() == userId.ToLower() && !c.IsCheckedOut);

            if (cart == null)
            {
                return isCartCheckedOut;
            }

            cart.IsCheckedOut = true;
            isCartCheckedOut = true;

            await cartRepository.SaveChangesAsync();

            return isCartCheckedOut;
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
            if (!Guid.TryParse(productId, out Guid productGuid))
                return;

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

