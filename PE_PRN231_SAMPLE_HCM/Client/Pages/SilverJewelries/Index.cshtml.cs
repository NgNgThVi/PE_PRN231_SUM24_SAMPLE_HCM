using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Client.Pages.Inheritance;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PE_PRN231_SAMPLE_HCM.Dtos;
using Microsoft.IdentityModel.Tokens;
using PE_PRN231_SAMPLE_HCM.Models;

namespace Client.Pages.SilverJewelries
{
    public class IndexModel : ClientAbstract
    {
        public IndexModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        private const int PageSize = 5;


        [BindProperty]
        public string? SilverJewelryName { get; set; } = null;
        [BindProperty]
        public decimal? MetalWeight { get; set; } = null;
        public IList<SilverJewelryDTO.GetSilverJewelryDTO> SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            var SilverJewelryName = Request.Query["SilverJewelryName"].ToString();
            var MetalWeight = Request.Query["MetalWeight"].ToString();
            if (SilverJewelryName.IsNullOrEmpty() && MetalWeight.IsNullOrEmpty())
            {
                string token = _context.HttpContext.Session.GetString("token");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await HttpClient.GetAsync("api/v1/silverjewelry");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    SilverJewelry = JsonConvert.DeserializeObject<List<SilverJewelryDTO.GetSilverJewelryDTO>>(content);

                    // paging
                    var count = SilverJewelry.Count();
                    var totalPages = (int)Math.Ceiling(count / (double)PageSize);
                    var items = SilverJewelry.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();

                    SilverJewelry = items;
                    PageIndex = pageIndex;
                    TotalPages = totalPages;
                }
                return Page();
            }
            else
            {
                string token = _context.HttpContext.Session.GetString("token");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var url = "api/v1/silverjewelry/search";
                if (!SilverJewelryName.IsNullOrEmpty() && MetalWeight.IsNullOrEmpty())
                {
                    url = $"api/v1/silverjewelry/search?SilverJewelryName={SilverJewelryName.Trim()}";
                }else if(SilverJewelryName.IsNullOrEmpty() && !MetalWeight.IsNullOrEmpty())
                {
                    url = $"api/v1/silverjewelry/search?MetalWeight={MetalWeight.Trim()}";
                }
                else
                {
                    url = $"api/v1/silverjewelry/search?SilverJewelryName={SilverJewelryName.Trim()}&MetalWeight={MetalWeight.Trim()}";
                }
                HttpResponseMessage response = await HttpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    SilverJewelry = JsonConvert.DeserializeObject<List<SilverJewelryDTO.GetSilverJewelryDTO>>(content);

                    // paging
                    var count = SilverJewelry.Count();
                    var totalPages = (int)Math.Ceiling(count / (double)PageSize);
                    var items = SilverJewelry.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();

                    SilverJewelry = items;
                    PageIndex = pageIndex;
                    TotalPages = totalPages;
                }

                return Page();

            }
        }
    }
}
