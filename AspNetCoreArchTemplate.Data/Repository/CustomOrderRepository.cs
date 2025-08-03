namespace AspNetCoreArchTemplate.Data.Repository
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    public class CustomOrderRepository
        : BaseRepository<CustomOrder, Guid>, ICustomOrderRepository
    {
        public CustomOrderRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
