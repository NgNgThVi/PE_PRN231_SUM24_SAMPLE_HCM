using Client.Pages.Inheritance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Client.Pages.Models;
using Newtonsoft.Json;

namespace Client.Pages
{
    public class LoginModel : ClientAbstract
    {
        public LoginModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            string url = "api/v1/auth/login";

            var request = new LoginModels
            {
                Email = username,
                Password = password
            };

            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //url = $"{url}?{queryString}";
            HttpResponseMessage respone = await HttpClient.PostAsync(url, httpContent);
            if (respone.IsSuccessStatusCode)
            {
                var content = await respone.Content.ReadAsStringAsync();

                string[] tokenParts = content.Split('.');
                string payloadBase64 = tokenParts[1].PadRight(tokenParts[1].Length + (4 - tokenParts[1].Length % 4) % 4, '=');
                // Thực hiện giải mã Base64
                byte[] payloadBytes = Convert.FromBase64String(payloadBase64);
               // byte[] payloadBytes = Convert.FromBase64String(payloadBase64);
                string payloadJson = Encoding.UTF8.GetString(payloadBytes);

                JsonDocument payload = JsonDocument.Parse(payloadJson);

                // Truy cập các trường trong payload
                string role = payload.RootElement.GetProperty("role").GetString();
                _context.HttpContext.Session.SetString("role", role);
                if (role.Equals("2") || role.Equals("1"))
                {
                    _context.HttpContext.Session.SetString("token", content);
                    return RedirectToPage("/SilverJewelries/Index");
                }
                else
                {
                    ViewData["Message"] = "You do not have permission to do this function, only Manager!";
                    return Page();
                }
            }
            ViewData["Message"] = "Wrong email or password!";
            return Page();
        }
    }
}
