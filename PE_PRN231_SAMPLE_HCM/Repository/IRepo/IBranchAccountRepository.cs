using BussinessObject.Models;
using PE_PRN231_SAMPLE_HCM.Dtos;

namespace Repository.IRepo
{
    public interface IBranchAccountRepository : IGenericRepository<BranchAccount>
    {
        public BranchAccount? Login(LoginDTO request);
    }
}
