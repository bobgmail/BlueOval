using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BlueOval.Login;

// --- Authentication Handler Definition (Same as before) ---
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    // ... [Implementation is the same as in the controller example] ...
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        string authorizationHeader = Request.Headers["Authorization"];
        if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Authorization header does not start with 'Basic'");
        }

        try
        {
            var credentials = authorizationHeader.Substring("Basic ".Length).Trim();
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            var parts = decodedCredentials.Split(':', 2);
            string username = parts[0];
            string password = parts[1];

            if (username == "gEiKyr1Z2QNbsP4xKbUpLwNY6GXR" && password == "lJZwiDZPuHsxi30KuYiWkjNqbmzPtXqo+y7e68IWWvs=")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
        }
    }
    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        // This method is called when an authentication challenge is needed.
        // It is responsible for sending the 401 response and the header.
        Response.StatusCode = 401;
        Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Secure Area\"");
    }
}
