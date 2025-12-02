var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// API backend URL (Render sẽ đọc từ ENV)
var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL")
                ?? "https://your-railway-api-url.up.railway.app/";

// Đăng ký HttpClient để gọi API
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// ép mở trang "/" về "/Index"
app.MapGet("/", context =>
{
    context.Response.Redirect("/Index");
    return Task.CompletedTask;
});

app.Run();
