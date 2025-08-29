

public interface IAccountLogic
{
    Task<(bool Success, string Message)> UserRegistrationAsync(RegisterVm register);
    Task<string> UserLoginAsyn(LoginVm loginVm);
    Task<string> UserLogoutAsyn();
    Task<bool> CheckUserLogin(LoginVm loginVm);
}