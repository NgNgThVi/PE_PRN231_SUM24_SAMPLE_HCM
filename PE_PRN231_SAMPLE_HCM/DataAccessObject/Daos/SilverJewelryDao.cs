using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject.Daos
{
    public class SilverJewelryDao : GenericDao<SilverJewelryDao>
    {
        public List<SilverJewelry> GetAll()
        {
            return _context.SilverJewelries.Include(x => x.Category).ToList();
        }
    }
}
