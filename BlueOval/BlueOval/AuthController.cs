

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAccountLogic _accountLogic;

    public AuthController(IAccountLogic accountLogic)
    {
        _accountLogic = accountLogic;
    }

    [HttpPost("login")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        string ret = await _accountLogic.UserLoginAsyn(vm);
        if (string.IsNullOrEmpty(ret))
            return Ok();
        else
            return Unauthorized();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _accountLogic.UserLogoutAsyn();
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        var result = await _accountLogic.UserRegistrationAsync(vm);
        if (result.Success)
            return Ok();
        else
            return BadRequest(result.Message);
    }

    [Authorize] // IMPORTANT: This endpoint must be protected
    [HttpGet("userinfo")]
    [IgnoreAntiforgeryToken]
    public IActionResult GetUserInfo()
    {
        // The [Authorize] attribute ensures this code only runs if the user is authenticated.
        // The user's claims are available via the HttpContext.
        var employeeIdClaim = User.FindFirst("EmployeeID");
        int.TryParse(employeeIdClaim?.Value, out var employeeId);
        var userInfo = new UserInfo
        {
            UserName = User.Identity.Name,
            Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray(),
            EmployeeID = employeeId
        };
        return Ok(userInfo);
    }
}