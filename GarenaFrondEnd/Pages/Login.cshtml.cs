using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarenaFrondEnd.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [BindProperty]
        public LoginDTO Login { get; set; } = new();

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _httpClient.PostAsJsonAsync("Register/login", Login);

            if (response.IsSuccessStatusCode)
            {
                // Có thể lưu token nếu backend trả về
                return RedirectToPage("/Login");
            }

            ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return Page();
        }
    }

}
