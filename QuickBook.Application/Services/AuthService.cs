using BCrypt.Net;
using QuickBook.Application.Dto.Login;
using QuickBook.Application.Dto.Register;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Users;
using QuickBook.Domain.Interface;
using QuickBook.Helper1;
using System.Security.Cryptography;

namespace QuickBook.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<RegisterReponsesDto> RegisterAsync(RegisterDto dto)
        {
            var emailExist = await _userRepository.GetByEmailAsync(dto.Email);

            if (emailExist != null)
                throw new ArgumentException("Email already exist");

            var userNameExist = await _userRepository.GetByUserNameAsync(dto.UserName);

            if(userNameExist != null)
                throw new ArgumentException("UserName already exist");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var createUser = new User(dto.UserName, dto.Email, hashedPassword, dto.Role);

            await _userRepository.AddAsync(createUser);

            return MaptoResponses(createUser);

        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
                throw new ArgumentException("Invalid email or password");

            if (user.IsLocked())
            {
                var remainingLockTime = (user.LockOutEnd!.Value - DateTime.UtcNow).Minutes;

                throw new ArgumentException($"Account is locked: try again {remainingLockTime} minutes");
            }


            bool password = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

           

            if(!password)
            {
                user.RecordFailedLoginAttempt();

                await _userRepository.UpdateAsync(user);

                throw new ArgumentException("Invalid email or password");
            }

            user.ResetFailedLoginAttempts();
            await _userRepository.UpdateAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, out DateTime expiresAt);
            var refreshToken = GenerateToken();

            user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }

       public  async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

            if (user == null)
                throw new ArgumentException("Invalid refresh token");

            if (user.RefreshTokenExpiryTime == null  || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new ArgumentException("Expired token");

            var newAccessToken = _jwtTokenGenerator.GenerateToken(user, out DateTime expiresAt);
            var newRefreshToken = GenerateToken();

            user.SetRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(7));
            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };

        }

        public async Task LogoutAsync(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

            if (user == null)
                throw new ArgumentException("Invalid token");

            user.RemoveRefreshToken();
            await _userRepository.UpdateAsync(user);
        }

        private static RegisterReponsesDto MaptoResponses(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Role = user.Role,
        };

       

        public static string GenerateToken(int size = 64)
        {
            var randomByte = RandomNumberGenerator.GetBytes(size);

            return Convert.ToBase64String(randomByte).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
