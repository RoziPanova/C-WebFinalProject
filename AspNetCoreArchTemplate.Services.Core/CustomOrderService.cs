namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.CustomOrder;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class CustomOrderService : ICustomOrderService
    {
        public readonly ICustomOrderRepository customOrderRepository;
        public CustomOrderService(ICustomOrderRepository customOrderRepository)
        {
            this.customOrderRepository = customOrderRepository;
        }
        public async Task<IEnumerable<CustomOrderListViewModel>> GetUserCustomOrdersAsync(string userId)
        {
            IEnumerable<CustomOrderListViewModel> customOrders = await customOrderRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(co => co.UserId == userId)
                .Select(co => new CustomOrderListViewModel
                {
                    Id = co.Id.ToString(),
                    CustomerName = co.UserName,
                    CustomerPhoneNumber = co.PhoneNumber,
                    CustomerAddress = co.Address,
                    CustomOrderNeededBy = co.RequestedDate,
                    CustomOrderDetails = co.Details,
                })
                .ToListAsync();
            return customOrders;
        }


        public async Task<bool> AddCustomOrderAsync(CustomOrderFormInputViewModel inputModel, string userId)
        {
            bool isCustomOrderCreated = false;
            CustomOrder newCustomOrder = new CustomOrder()
            {
                UserId = userId,
                UserName = inputModel.UserName,
                PhoneNumber = inputModel.PhoneNumber,
                Address = inputModel.Address,
                Details = inputModel.Details,
                RequestedDate = DateOnly
                .ParseExact(inputModel.RequestedDate, AppDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None),
            };

            await this.customOrderRepository.AddAsync(newCustomOrder);
            await this.customOrderRepository.SaveChangesAsync();
            isCustomOrderCreated = true;
            return isCustomOrderCreated;
        }
        public async Task<CustomOrderFormInputViewModel?> GetCustomOrderForEditAsync(string customOrderId)
        {
            if (!Guid.TryParse(customOrderId, out Guid customOrderIdGuid))
                return null;

            var order = await customOrderRepository
                .GetAllAttached()
                .SingleOrDefaultAsync(o => o.Id == customOrderIdGuid);

            if (order == null)
                return null;

            CustomOrderFormInputViewModel customOrder =
                new CustomOrderFormInputViewModel()
                {
                    UserName = order.UserName,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    RequestedDate = order.RequestedDate.ToString(AppDateFormat),
                    Details = order.Details
                };
            return customOrder;
        }

        public async Task<bool> UpdateCustomOrderAsync(string customOrderId, CustomOrderFormInputViewModel model)
        {
            bool isCustomOrderUpdated = false;
            if (!Guid.TryParse(customOrderId, out Guid customOrderIdGuid))
                return isCustomOrderUpdated;

            var order = await customOrderRepository
                .GetAllAttached()
                .SingleOrDefaultAsync(o => o.Id == customOrderIdGuid);

            if (order == null)
                return isCustomOrderUpdated;

            // Check if the needed-by date is too close
            if (order.RequestedDate <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)))
            {
                // reject update
                return isCustomOrderUpdated;
            }

            order.UserName = model.UserName;
            order.PhoneNumber = model.PhoneNumber;
            order.Address = model.Address;
            order.RequestedDate = DateOnly.ParseExact(model.RequestedDate, AppDateFormat);
            order.Details = model.Details;

            await customOrderRepository.UpdateAsync(order);
            await customOrderRepository.SaveChangesAsync();
            isCustomOrderUpdated = true;
            return isCustomOrderUpdated;
        }

    }
}
