namespace AspNetCoreArchTemplate.Data.Repository
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;

    public class CartRepository
        : BaseRepository<Cart, Guid>, ICartRepository
    {
        public CartRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
