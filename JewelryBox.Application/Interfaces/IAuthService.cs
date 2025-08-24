using JewelryBox.Application.DTOs;

namespace JewelryBox.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
    }
}
