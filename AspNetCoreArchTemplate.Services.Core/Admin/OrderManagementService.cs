namespace AspNetCoreArchTemplate.Services.Core.Admin
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement;
    using Microsoft.EntityFrameworkCore;

    public class OrderManagementService : IOrderManagementService
    {
        private readonly ICartRepository cartRepository;
        private readonly ICartItemsRepository cartItemsRepository;
        private readonly ICustomOrderRepository customOrderRepository;

        public OrderManagementService(
            ICartRepository cartRepository,
            ICartItemsRepository cartItemsRepository,
            ICustomOrderRepository customOrderRepository)
        {
            this.cartRepository = cartRepository;
            this.cartItemsRepository = cartItemsRepository;
            this.customOrderRepository = customOrderRepository;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            IEnumerable<OrderViewModel> orders = await cartRepository
                .GetAllAttached()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(c => c.IsCheckedOut == true)
                .Select(c => new OrderViewModel()
                {
                    Id = c.Id.ToString(),
                    CustomerName = c.User.UserName!,
                    TotalItemsCount = c.Items.Sum(i => i.Quantity),
                    TotalAmount = c.Items.Sum(i => i.Quantity * i.Product.Price)
                })
                .ToArrayAsync();

            return orders;
        }

        public async Task<IEnumerable<CustomOrderViewModel>> GetAllCustomOrdersAsync()
        {
            IEnumerable<CustomOrderViewModel> customOrders = await customOrderRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(o => new CustomOrderViewModel()
                {
                    Id = o.Id.ToString(),
                    CustomerName = o.UserName,
                    CustomerPhoneNumber = o.PhoneNumber,
                    CustomerAddress = o.Address,
                    CustomOrderNeededBy = o.RequestedDate,
                    CustomOrderDetails = o.Details,
                })
                .ToArrayAsync();

            return customOrders;
        }
        public async Task<bool> DeleteOrderAsync(string id)
        {
            bool isOrderDeletedSuccessfully = false;
            if (!Guid.TryParse(id, out Guid orderId))
                return isOrderDeletedSuccessfully;

            var order = await cartRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Include(c => c.Items)
                .SingleOrDefaultAsync(c => c.Id == orderId);

            if (order == null)
                return isOrderDeletedSuccessfully;


            foreach (var item in order.Items.ToList())
            {
                await cartItemsRepository.HardDeleteAsync(item);
            }

            await cartRepository.HardDeleteAsync(order);

            await cartRepository.SaveChangesAsync();
            await cartItemsRepository.SaveChangesAsync();

            isOrderDeletedSuccessfully = true;
            return isOrderDeletedSuccessfully;
        }


        public async Task<bool> DeleteCustomOrderAsync(string id)
        {
            bool isCustomOrderDeletedSuccessfully = false;
            if (!Guid.TryParse(id, out Guid customOrderId))
                return isCustomOrderDeletedSuccessfully;

            var customOrder = await customOrderRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(o => o.Id == customOrderId);

            if (customOrder == null)
                return isCustomOrderDeletedSuccessfully;

            await customOrderRepository.HardDeleteAsync(customOrder);
            await customOrderRepository.SaveChangesAsync();

            isCustomOrderDeletedSuccessfully = true;
            return isCustomOrderDeletedSuccessfully;
        }

    }
}