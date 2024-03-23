using BussinessObject.Models;
using PE_PRN231_SAMPLE_HCM.Dtos;

namespace DataAccessObject.Daos
{
    public class BranchDao : GenericDao<BranchAccount>
    {
        public BranchAccount? Login(LoginDTO request)
        {
            return _context.BranchAccounts.Where(x => x.EmailAddress.ToLower() == request.Email &&
                                                      x.AccountPassword == request.Password).FirstOrDefault();
        }
    }
}
