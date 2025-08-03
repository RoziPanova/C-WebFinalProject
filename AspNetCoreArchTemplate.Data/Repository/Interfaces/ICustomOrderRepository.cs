using AspNetCoreArchTemplate.Data.Models;

namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    public interface ICustomOrderRepository
        : IRepository<CustomOrder, Guid>, IAsyncRepository<CustomOrder, Guid>
    {
    }
}
