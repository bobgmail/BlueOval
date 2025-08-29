
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public ApiAuthenticationStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // This is the core method that Blazor calls to get the user's identity.
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userInfo = await _httpClient.GetFromJsonAsync<UserInfo>("api/auth/userinfo");

            if (userInfo != null)
            {
                // Create the claims principal from the user info received from the server.
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userInfo.UserName),
                    new Claim("EmployeeID", userInfo.EmployeeID.ToString())

                }, "apiauth");

                foreach (var role in userInfo.Roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            // If the server returns 401 Unauthorized, the user is not logged in.
        }

        // If anything fails or the user is not authenticated, return an empty identity.
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    // This is our custom method to notify Blazor that the auth state has changed.
    public void NotifyUserAuthentication()
    {
        // This built-in Blazor method tells all <AuthorizeView> components
        // and other auth-aware parts of the app to re-run GetAuthenticationStateAsync().
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void NotifyUserLogout()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}

