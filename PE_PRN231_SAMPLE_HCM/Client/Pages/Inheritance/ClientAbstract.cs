using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages.Inheritance
{
    public class ClientAbstract : PageModel
    {
        private readonly IHttpClientFactory _http;
        public HttpClient HttpClient { get; }
        public readonly IHttpContextAccessor _context;

        public ClientAbstract(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _context = httpContextAccessor;
            HttpClient = _http.CreateClient();
            HttpClient.BaseAddress = new Uri("https://localhost:7078/");
        }

        public bool CheckAuthen()
        {
            var str = _context.HttpContext.Session.GetString("role");
            return str != null
                 && (str.Equals("1") || str.Equals("2"));
        }
    }
}
