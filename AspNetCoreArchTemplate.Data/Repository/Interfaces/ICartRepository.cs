namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    using AspNetCoreArchTemplate.Data.Models;

    public interface ICartRepository :
        IRepository<Cart, Guid>, IAsyncRepository<Cart, Guid>
    {
    }
}
