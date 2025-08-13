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
                    RequestedDate = co.RequestedDate,
                    Details = co.Details,
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

            var customOrder = await customOrderRepository
                .GetAllAttached()
                .SingleOrDefaultAsync(o => o.Id == customOrderIdGuid);

            if (customOrder == null)
                return null;

            CustomOrderFormInputViewModel customOrderModel =
                new CustomOrderFormInputViewModel()
                {
                    UserName = customOrder.UserName,
                    PhoneNumber = customOrder.PhoneNumber,
                    Address = customOrder.Address,
                    RequestedDate = customOrder.RequestedDate.ToString(AppDateFormat),
                    Details = customOrder.Details
                };
            return customOrderModel;
        }

        public async Task<bool> UpdateCustomOrderAsync(string customOrderId, CustomOrderFormInputViewModel model)
        {
            bool isCustomOrderUpdated = false;
            if (!Guid.TryParse(customOrderId, out Guid customOrderIdGuid))
                return isCustomOrderUpdated;

            var customOrder = await customOrderRepository
                .GetAllAttached()
                .SingleOrDefaultAsync(o => o.Id == customOrderIdGuid);

            if (customOrder == null)
                return isCustomOrderUpdated;

            // Check if the needed-by date is too close
            if (customOrder.RequestedDate <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)))
            {
                // reject update
                return isCustomOrderUpdated;
            }

            customOrder.UserName = model.UserName;
            customOrder.PhoneNumber = model.PhoneNumber;
            customOrder.Address = model.Address;
            customOrder.RequestedDate = DateOnly.ParseExact(model.RequestedDate, AppDateFormat);
            customOrder.Details = model.Details;

            await customOrderRepository.UpdateAsync(customOrder);
            await customOrderRepository.SaveChangesAsync();
            isCustomOrderUpdated = true;
            return isCustomOrderUpdated;
        }
        public async Task<CustomOrderDetailsViewModel?> GetCustomOrderDetailsAsync(string customOrderId)
        {
            if (!Guid.TryParse(customOrderId, out Guid customOrderIdGuid))
                return null;

            var customOrder = await customOrderRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == customOrderIdGuid);

            if (customOrder == null)
                return null;

            CustomOrderDetailsViewModel viewModel =
                new CustomOrderDetailsViewModel()
                {
                    UserName = customOrder.UserName,
                    PhoneNumber = customOrder.PhoneNumber,
                    Address = customOrder.Address,
                    RequestedDate = customOrder.RequestedDate,
                    Details = customOrder.Details,
                };

            return viewModel;
        }
        public async Task<bool> DeleteCustomOrderAsync(string customOrderId)
        {
            bool isCustomOrderDeleted = false;
            if (!Guid.TryParse(customOrderId, out Guid customOrderIdGuid))
                return isCustomOrderDeleted;

            var customOrder = await customOrderRepository
                .GetAllAttached()
                .SingleOrDefaultAsync(o => o.Id == customOrderIdGuid);

            if (customOrder == null)
                return isCustomOrderDeleted;

            if (customOrder.RequestedDate <= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)))
            {
                return isCustomOrderDeleted;
            }


            await customOrderRepository.HardDeleteAsync(customOrder);
            await customOrderRepository.SaveChangesAsync();

            isCustomOrderDeleted = true;
            return isCustomOrderDeleted;
        }

    }
}
