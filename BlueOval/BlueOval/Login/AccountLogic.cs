
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography;




public class AccountLogic : IAccountLogic
{
    private readonly IHttpContextAccessor _accessor;
    DapperContext sqlService;
    

    public AccountLogic(IHttpContextAccessor accessor, DapperContext _sqlService)
    {
        _accessor = accessor;
        sqlService = _sqlService;
       
    }

   

    public async Task<(bool Success, string Message)> UserRegistrationAsync(RegisterVm register)
    {
        return (false, string.Empty);
    }

   

    public async Task<string> UserLoginAsyn(LoginVm loginVm)
    {
        AspNetUsers? user = await sqlService.GetUserInfoFromNetCore(loginVm.Name);

        AspNetUsers ? AspUser = await sqlService.GetUserInfoFromHIS(loginVm.Name);

        SqlPasswordHasher passwordHasher = new SqlPasswordHasher();
      
        
        
        if (user == null || passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginVm.Password) == PasswordVerificationResult.Failed)
        {
            if (AspUser == null || passwordHasher.VerifyHashedPassword(AspUser, AspUser.PasswordHash, loginVm.Password) == PasswordVerificationResult.Failed)
            {
                return "Invalid Credentials";
            }

            user = AspUser;
        }
       
      

        int EmpID = await sqlService.GetEmplyeeID(user.Id);
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, $"{user.UserName}"));//$"{user.FirstName} {user.LastName}"));
        claims.Add(new Claim("EmployeeID", EmpID.ToString()));//$"{user.FirstName} {user.LastName}"));
                                                              // claims.Add(new Claim("Site", loginVm.Site));
        var userRoles = await sqlService.GetUserRoles(user.Id);
        foreach (var RoleName in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, RoleName));
        }

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        { };
        var principal = new ClaimsPrincipal(claimsIdentity);

 

        await _accessor.HttpContext.SignInAsync(
           CookieAuthenticationDefaults.AuthenticationScheme,
           principal,
           authProperties);

 
        return string.Empty;
    }

    public async Task<bool> CheckUserLogin(LoginVm loginVm)
    {
       
        AspNetUsers? AspUser = await sqlService.GetUserInfoFromHIS(loginVm.Name);
        if(AspUser== null) return false;

        SqlPasswordHasher passwordHasher = new SqlPasswordHasher();
        if (passwordHasher.VerifyHashedPassword(AspUser, AspUser.PasswordHash, loginVm.Password) == PasswordVerificationResult.Failed)
            return false;

        return true;
    }

    public async Task<string> UserLogoutAsyn()
    {


        await _accessor.HttpContext.SignOutAsync();

        return string.Empty;
    }
}