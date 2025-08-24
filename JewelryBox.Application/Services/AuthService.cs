using BCrypt.Net;
using JewelryBox.Application.DTOs;
using JewelryBox.Application.Interfaces;
using JewelryBox.Domain.Entities;
using JewelryBox.Domain.Interfaces;

namespace JewelryBox.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Check if user already exists
                if (await _userRepository.ExistsAsync(request.Email))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "User with this email already exists."
                    };
                }

                if (await _userRepository.ExistsByUsernameAsync(request.Username))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Username is already taken."
                    };
                }

                // Create new user
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var createdUser = await _userRepository.CreateAsync(user);

                // Generate token
                var userDto = new UserDto
                {
                    Id = createdUser.Id,
                    Username = createdUser.Username,
                    Email = createdUser.Email,
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    CreatedAt = createdUser.CreatedAt
                };

                var token = _jwtService.GenerateToken(userDto);
                var refreshToken = _jwtService.GenerateRefreshToken();

                return new AuthResponse
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                    Message = "User registered successfully.",
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                // Find user by username
                var user = await _userRepository.GetByUsernameAsync(request.Username);
                if (user == null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid username or password."
                    };
                }

                // Verify password
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid username or password."
                    };
                }

                // Update last login
                await _userRepository.UpdateLastLoginAsync(user.Id);

                // Generate token
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedAt = user.CreatedAt
                };

                var token = _jwtService.GenerateToken(userDto);
                var refreshToken = _jwtService.GenerateRefreshToken();

                return new AuthResponse
                {
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                    Message = "Login successful.",
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = $"Login failed: {ex.Message}"
                };
            }
        }

        public Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            // For Phase 1, we'll implement a simple refresh token validation
            // In a production environment, you'd want to store refresh tokens in the database
            return Task.FromResult(new AuthResponse
            {
                Success = false,
                Message = "Refresh token functionality not implemented in Phase 1."
            });
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            var principal = _jwtService.ValidateToken(token);
            return Task.FromResult(principal != null);
        }
    }
}
