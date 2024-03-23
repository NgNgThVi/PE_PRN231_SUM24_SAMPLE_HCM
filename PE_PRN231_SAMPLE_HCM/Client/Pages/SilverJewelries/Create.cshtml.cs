using BussinessObject.Models;
using Client.Pages.Inheritance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Pages.SilverJewelries
{
    public class CreateModel : ClientAbstract
    {
        public CreateModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            var str = _context.HttpContext.Session.GetString("role");
            if(str.Equals("2"))
            {
                return RedirectToPage("Index");
            }
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync("api/v1/category");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var temp = JsonConvert.DeserializeObject<List<Category>>(content);
                ViewData["CategoryId"] = new SelectList(temp, "CategoryId", "CategoryName");
            }
            return Page();
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = "api/v1/silverjewelry";
            var jsonContent = JsonConvert.SerializeObject(SilverJewelry);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Message"] = "Create Fail: " + await response.Content.ReadAsStringAsync();
                await OnGet();
                return Page();
            }
        }
    }
}
