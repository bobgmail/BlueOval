using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
public class AuthService
{
    private readonly HttpClient _httpClient;
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<bool> Login(LoginVm loginVm)
    {
        // Call the server's API endpoint
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginVm);

        // Return true if the login was successful (2xx status code)
        return response.IsSuccessStatusCode;
    }
    public async Task Logout()
    {
        await _httpClient.PostAsync("api/auth/logout", null);
    }
}