using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.IRepo;

namespace PE_PRN231_SAMPLE_HCM.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    [Authorize(Roles = "1")]
    public class CategoryController : ControllerBase
    {
        private readonly ISilverJewelryRepository _repo;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ISilverJewelryRepository repo, ICategoryRepository categoryRepository)
        {
            _repo = repo;
            _categoryRepository = categoryRepository;
        }
        [EnableQuery]
        [HttpGet]
        public async Task<List<Category>> GetAll()
        {
            return _categoryRepository.GetAll();
        }
    }
}
