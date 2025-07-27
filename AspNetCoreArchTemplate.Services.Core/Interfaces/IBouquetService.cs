namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.Bouquet;

    public interface IBouquetService
    {
        Task<IEnumerable<BouquetIndexViewModel>> GetAllBouquetsAsync();
    }
}
