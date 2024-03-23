using BussinessObject.Models;
using DataAccessObject;
using Repository.IRepo;

namespace Repository.Repo
{
    public class SilverJewelryRepository : GenericRepository<SilverJewelry>, ISilverJewelryRepository
    {
        public SilverJewelryRepository()
        {
        }
    }
}
