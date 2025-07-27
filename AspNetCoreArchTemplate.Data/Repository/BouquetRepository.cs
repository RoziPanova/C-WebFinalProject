namespace AspNetCoreArchTemplate.Data.Repository
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using CinemaApp.Data.Repository;

    public class BouquetRepository :
        BaseRepository<Bouquet, Guid>, IBouquetRepository
    {
        public BouquetRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
