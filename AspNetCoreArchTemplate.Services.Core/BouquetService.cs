namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Bouquet;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class BouquetService : IBouquetService
    {
        private readonly IBouquetRepository bouquetRepository;

        public BouquetService(IBouquetRepository bouquetRepository)
        {
            this.bouquetRepository = bouquetRepository;
        }
        public async Task<IEnumerable<BouquetIndexViewModel>> GetAllBouquetsAsync()
        {

            IEnumerable<BouquetIndexViewModel> allBouquets = await this.bouquetRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(b => new BouquetIndexViewModel()
                {
                    Id = b.Id.ToString(),
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                })
                .ToListAsync();
            foreach (BouquetIndexViewModel bouquet in allBouquets)
            {
                if (String.IsNullOrEmpty(bouquet.ImageUrl))
                {
                    bouquet.ImageUrl = $"{NoImageUrl}";
                }
            }
            return allBouquets;
        }
    }
}

