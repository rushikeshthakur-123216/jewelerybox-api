using Microsoft.AspNetCore.Mvc;
using JewelryBox.Application.DTOs;
using JewelryBox.Application.Interfaces;
using JewelryBox.Infrastructure.Data;
using Dapper;

namespace JewelryBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IDbConnectionFactory _connectionFactory;

        public AuthController(IAuthService authService, IDbConnectionFactory connectionFactory)
        {
            _authService = authService;
            _connectionFactory = connectionFactory;
        }

        [HttpGet("test-connection")]
        public async Task<ActionResult<object>> TestConnection()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                
                // Test a simple query
                var result = await connection.QuerySingleAsync<int>("SELECT 1");
                
                return Ok(new 
                { 
                    success = true, 
                    message = "Database connection successful!",
                    testQuery = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = $"Database connection failed: {ex.Message}",
                    error = ex.ToString()
                });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.RegisterAsync(request);
            
            if (response.Success)
            {
                return Ok(response);
            }
            
            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.LoginAsync(request);
            
            if (response.Success)
            {
                return Ok(response);
            }
            
            return Unauthorized(response);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] string refreshToken)
        {
            var response = await _authService.RefreshTokenAsync(refreshToken);
            
            if (response.Success)
            {
                return Ok(response);
            }
            
            return BadRequest(response);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateToken([FromBody] string token)
        {
            var isValid = await _authService.ValidateTokenAsync(token);
            return Ok(isValid);
        }
    }
}
