using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using PE_PRN231_SAMPLE_HCM.Dtos;
using Repository.IRepo;
using Repository.Repo;
using Repository.Token;

namespace PE_PRN231_SAMPLE_HCM.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBranchAccountRepository _repo;
        private IConfiguration _config;

        public AuthController(IBranchAccountRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
            ProvideToken.Initialize(_config);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var account = _repo.Login(request);
            if(account == null)
            {
                return Unauthorized("Email or password wrong, please try login!");
            }
            var token = ProvideToken.Instance.GenerateToken(account);
            return Ok(token);
        }
    }
}
