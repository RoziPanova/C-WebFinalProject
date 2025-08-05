namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    using AspNetCoreArchTemplate.Data.Models;

    public interface ICartItemsRepository
        : IRepository<CartItem, Guid>, IAsyncRepository<CartItem, Guid>
    {
    }
}
