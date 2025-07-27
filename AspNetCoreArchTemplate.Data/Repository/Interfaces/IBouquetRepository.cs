using AspNetCoreArchTemplate.Data.Models;

namespace AspNetCoreArchTemplate.Data.Repository.Interfaces
{
    public interface IBouquetRepository :
        IRepository<Bouquet, Guid>, IAsyncRepository<Bouquet, Guid>
    {

    }
}
