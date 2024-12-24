using MrKatsuWeb.DTO.Users;

namespace MrKatsuWeb.Application.Interfaces.System
{
    public interface IUserService
    {
        Task<string?> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
