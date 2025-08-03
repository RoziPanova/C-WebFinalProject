namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.CustomOrder;
    using System.Globalization;
    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class CustomOrderService : ICustomOrderService
    {
        public readonly ICustomOrderRepository customOrderRepository;
        public CustomOrderService(ICustomOrderRepository customOrderRepository)
        {
            this.customOrderRepository = customOrderRepository;
        }

        public async Task AddCustomOrderAsync(CustomOrderFormInputViewModel inputModel)
        {
            CustomOrder newCustomOrder = new CustomOrder()
            {
                UserName = inputModel.UserName,
                PhoneNumber = inputModel.PhoneNumber,
                Address = inputModel.Address,
                Details = inputModel.Details,
                RequestedDate = DateOnly
                .ParseExact(inputModel.RequestedDate, AppDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None),
            };

            await this.customOrderRepository.AddAsync(newCustomOrder);
        }
    }
}
