using Microsoft.EntityFrameworkCore;
using ShopifyApi.Data;
using ShopifyApi.DTOs.Auth;
using ShopifyApi.Interfaces;
using ShopifyApi.Models;

namespace ShopifyApi.Services
{
    public class AuthService: IAuthService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists) throw new InvalidOperationException("Email already registered");

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) 
                throw new KeyNotFoundException("Invalid email or password");

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
