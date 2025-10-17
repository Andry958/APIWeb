using BusinessLogic.DTOs.Users;

namespace BusinessLogic.Interfaces
{
    public interface IUserServices
    {
        public Task Register(UserRegisterModel model);
        public Task<LoginResponse> Login(UserLoginModel model, string? ipAddress);
        public Task Logout();
        public Task<List<UserGetModel>> GetAll();
        public Task<string> ForgetPssword(string Email);
        public Task ResetPassword(string email, string token, string newPassword);
        public Task<LoginResponse> Refresh(RefreshRequest model, string? ipAddress);
        public Task Delete(string id);

    }
}
