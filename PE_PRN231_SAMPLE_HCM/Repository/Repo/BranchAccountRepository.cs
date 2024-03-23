using BussinessObject.Models;
using DataAccessObject;
using DataAccessObject.Daos;
using PE_PRN231_SAMPLE_HCM.Dtos;
using Repository.IRepo;

namespace Repository.Repo
{
    public class BranchAccountRepository : GenericRepository<BranchAccount>, IBranchAccountRepository
    {
        private BranchDao _dao;
        public BranchAccountRepository()
        {
            _dao = new BranchDao();
        }

        public BranchAccount? Login(LoginDTO request)
        {
            return _dao.Login(request);
        }
    } 
}
