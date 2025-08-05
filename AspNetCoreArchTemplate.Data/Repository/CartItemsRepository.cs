namespace AspNetCoreArchTemplate.Data.Repository
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;

    public class CartItemsRepository : BaseRepository<CartItem, Guid>, ICartItemsRepository
    {
        public CartItemsRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
