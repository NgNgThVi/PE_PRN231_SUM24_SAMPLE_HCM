using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PE_PRN231_SAMPLE_HCM.Dtos;
using PE_PRN231_SAMPLE_HCM.Models;
using Repository.IRepo;
using System.Linq.Expressions;

namespace PE_PRN231_SAMPLE_HCM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SilverJewelryController : ControllerBase
    {
        private readonly ISilverJewelryRepository _repo;
        private readonly ICategoryRepository _categoryRepository;

        public SilverJewelryController(ISilverJewelryRepository repo, ICategoryRepository categoryRepository)
        {
            _repo = repo;
            _categoryRepository = categoryRepository;
        }
        [EnableQuery]
        [HttpGet]
        [Authorize(Roles = ("1,2"))]
        public async Task<List<SilverJewelryDTO.GetSilverJewelryDTO>> GetAll()
        {
            var listSil = _repo.GetAll();
            var listResult = new List<SilverJewelryDTO.GetSilverJewelryDTO>();
            foreach (var item in listSil)
            {
                var result = new SilverJewelryDTO.GetSilverJewelryDTO()
                {
                    CategoryId = item.CategoryId,
                    CreatedDate = item.CreatedDate,
                    MetalWeight = item.MetalWeight,
                    Price = item.Price,
                    ProductionYear = item.ProductionYear,
                    SilverJewelryDescription = item.SilverJewelryDescription,
                    SilverJewelryId = item.SilverJewelryId,
                    SilverJewelryName = item.SilverJewelryName,
                };
                var category = _categoryRepository.GetByStringId(result.CategoryId);
                if (category != null)
                {
                    result.CategoryName = category.CategoryName;
                }
                listResult.Add(result);
            }
            return listResult;
        }
        [EnableQuery]
        [HttpGet]
        [Route("search")]
        [Authorize(Roles = ("1,2"))]
        public async Task<List<SilverJewelryDTO.GetSilverJewelryDTO>> SearchAll([FromQuery]SearchModels request)
        {
            var listSil = _repo.GetAll();

            if (!string.IsNullOrEmpty(request.SilverJewelryName))
            {
                listSil = listSil.Where(s => s.SilverJewelryName.ToLower().Contains(request.SilverJewelryName.Trim().ToLower())).ToList();
            }

            if (request.MetalWeight.HasValue)
            {
                listSil = listSil.Where(s => s.MetalWeight == request.MetalWeight).ToList();
            }
            var listResult = new List<SilverJewelryDTO.GetSilverJewelryDTO>();
            foreach (var item in listSil)
            {
                var result = new SilverJewelryDTO.GetSilverJewelryDTO()
                {
                    CategoryId = item.CategoryId,
                    CreatedDate = item.CreatedDate,
                    MetalWeight = item.MetalWeight,
                    Price = item.Price,
                    ProductionYear = item.ProductionYear,
                    SilverJewelryDescription = item.SilverJewelryDescription,
                    SilverJewelryId = item.SilverJewelryId,
                    SilverJewelryName = item.SilverJewelryName,
                };
                var category = _categoryRepository.GetByStringId(result.CategoryId);
                if (category != null)
                {
                    result.CategoryName = category.CategoryName;
                }
                listResult.Add(result);
            }
            return listResult;
        }
        [HttpPost]
        [Authorize(Roles = ("1"))]
        public async Task<IActionResult> Create(SilverJewelryDTO.CreateSilverJewelryDTO request)
        {
            var category = _categoryRepository.GetByStringId(request.CategoryId);
            if (category != null)
            {
                return BadRequest("Not found this category id");
            }
            var sil = new SilverJewelry()
            {
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.Now,
                MetalWeight = request.MetalWeight,
                Price = request.Price,
                ProductionYear = request.ProductionYear,
                SilverJewelryDescription = request.SilverJewelryDescription,
                SilverJewelryId = request.SilverJewelryId,
                SilverJewelryName = request.SilverJewelryName
            };
            try
            {
                _repo.Add(sil);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Create successfully");
        }
    }
}
