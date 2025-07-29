namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    using AspNetCoreArchTemplate.Data.Models;

    public interface IOrderItemRepository :
        IRepository<OrderItem, Guid>, IAsyncRepository<OrderItem, Guid>
    {
    }
}
