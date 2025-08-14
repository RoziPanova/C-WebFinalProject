namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.Search;

    public interface ISearchService
    {
        Task<SearchResultsViewModel> SearchAsync(string query);
    }
}
