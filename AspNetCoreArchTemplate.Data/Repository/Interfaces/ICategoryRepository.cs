namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    using AspNetCoreArchTemplate.Data.Models;

    public interface ICategoryRepository :
        IRepository<Category, Guid>, IAsyncRepository<Category, Guid>
    {
    }
}
