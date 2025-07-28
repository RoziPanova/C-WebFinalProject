namespace AspNetCoreArchTemplate.Data.Repository
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    public class ArrangementRepository
        : BaseRepository<Arrangement, Guid>, IArrangementRepository
    {
        public ArrangementRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
