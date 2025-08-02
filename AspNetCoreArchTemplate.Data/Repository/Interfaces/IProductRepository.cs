namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    using AspNetCoreArchTemplate.Data.Models;

    public interface IProductRepository :
        IRepository<Product, Guid>, IAsyncRepository<Product, Guid>
    {

    }
}
