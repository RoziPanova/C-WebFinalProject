namespace AspNetCoreArchTemplate.Data.Repository
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;

    public class OrderItemRepository : BaseRepository<OrderItem, Guid>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
