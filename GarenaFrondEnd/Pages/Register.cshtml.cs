using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GarenaFrondEnd.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [BindProperty]
        public RegisterDTO Register { get; set; } = new();

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _httpClient.PostAsJsonAsync("Register/register", Register);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Login");
            }

            ErrorMessage = "Đăng ký thất bại. Vui lòng thử lại.";
            return Page();
        }
    }
}
