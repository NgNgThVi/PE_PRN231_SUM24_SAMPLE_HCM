using BussinessObject.Models;
using Repository.IRepo;

namespace Repository.Repo
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository()
        {
        }
    }
}
