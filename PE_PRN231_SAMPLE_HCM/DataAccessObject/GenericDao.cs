using BussinessObject.Models;

namespace DataAccessObject
{
    public class GenericDao<T> where T : class
    {
        public SilverJewelry2023DbContext _context;

        private GenericDao<T> _instance = null;
        public GenericDao<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GenericDao<T>();
                }
                return _instance;
            }
            set { _instance = value; 
            }
        }
        public GenericDao()
        {
            _context = new SilverJewelry2023DbContext();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public virtual T? GetByStringId(string id)
        {
            return _context.Set<T>().Find(id);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            Save();
        }
    }
}
