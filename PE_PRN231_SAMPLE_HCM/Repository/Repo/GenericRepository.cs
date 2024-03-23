using DataAccessObject;
using Repository.IRepo;

namespace Repository.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private GenericDao<T> _dao;

        public GenericRepository()
        {
            _dao = new GenericDao<T>();
        }

        public void Add(T entity)
        {
            _dao.Add(entity);
        }

        public void Delete(T entity)
        {
            _dao.Delete(entity);
        }

        public List<T> GetAll()
        {
            return _dao.GetAll();
        }

        public T? GetById(int id)
        {
            return _dao.GetById(id);
        }

        public T? GetByStringId(string id)
        {
            return _dao.GetByStringId(id);
        }

        public int Save()
        {
            return (_dao.Save());
        }

        public void Update(T entity)
        {
            _dao.Update(entity);
        }
    }

}
