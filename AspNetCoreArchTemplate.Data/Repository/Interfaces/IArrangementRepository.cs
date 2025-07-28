namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    using AspNetCoreArchTemplate.Data.Models;

    public interface IArrangementRepository
        : IRepository<Arrangement, Guid>, IAsyncRepository<Arrangement, Guid>
    {
    }
}
