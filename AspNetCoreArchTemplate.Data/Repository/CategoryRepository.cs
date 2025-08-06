using AspNetCoreArchTemplate.Data.Models;
using AspNetCoreArchTemplate.Data.Repository.Interfaces;

namespace AspNetCoreArchTemplate.Data.Repository
{
    class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
