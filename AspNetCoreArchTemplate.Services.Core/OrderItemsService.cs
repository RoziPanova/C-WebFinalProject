namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.OrderItems;
    using Microsoft.EntityFrameworkCore;

    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class OrderItemsService : IOrderItemsService
    {
        private readonly IOrderItemRepository orderItemsRepository;
        private readonly IBouquetRepository bouquetRepository;
        private readonly IArrangementRepository arrangementRepository;
        public OrderItemsService(IOrderItemRepository orderItemsRepository, IBouquetRepository bouquetRepository, IArrangementRepository arrangementRepository)
        {
            this.orderItemsRepository = orderItemsRepository;
            this.bouquetRepository = bouquetRepository;
            this.arrangementRepository = arrangementRepository;
        }

        public async Task<IEnumerable<OrderItemsIndexViewModel>> GetAllOrderedItemsAync()
        {
            IEnumerable<OrderItemsIndexViewModel> allItems = new List<OrderItemsIndexViewModel>();

            allItems = await this.orderItemsRepository
             .GetAllAttached()
             .AsNoTracking()
             .Select(o => new OrderItemsIndexViewModel()
             {
                 Id = o.Id.ToString(),
                 Name = o.Bouquet.Name ?? o.Arrangement.Name ?? "Custom Order",
                 ImageUrl = o.Bouquet.ImageUrl ?? o.Arrangement.ImageUrl ?? $"{NoImageUrl}",
                 Quantity = o.Quantity,
                 Price = (o.Bouquet.Price.ToString() ?? o.Arrangement.Price.ToString()) ?? "Custom Order",
             })
             .ToListAsync();

            return allItems;
        }

        public async Task AddItemToCart(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            Guid guidId = Guid.Parse(id);
            OrderItem orderItem;

            var bouquet = await bouquetRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == guidId);

            if (bouquet != null)
            {
                orderItem = new OrderItem
                {
                    BouquetId = bouquet.Id,
                    Quantity = 1
                };
            }
            else
            {
                var arrangement = await arrangementRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == guidId);

                if (arrangement != null)
                {
                    orderItem = new OrderItem
                    {
                        ArrangementId = arrangement.Id,
                        Quantity = 1
                    };
                }
                else
                {
                    orderItem = new OrderItem
                    {

                        CustomOrderId = guidId,
                        Quantity = 1
                    };

                }

            }

            await orderItemsRepository.AddAsync(orderItem);
            await orderItemsRepository.SaveChangesAsync();
        }
    }
}
