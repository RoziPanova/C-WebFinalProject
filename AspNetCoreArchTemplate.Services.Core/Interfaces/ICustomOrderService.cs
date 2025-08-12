using AspNetCoreArchTemplate.Web.ViewModels.CustomOrder;

namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    public interface ICustomOrderService
    {
        public Task<bool> AddCustomOrderAsync(CustomOrderFormInputViewModel inputModel);
    }
}
