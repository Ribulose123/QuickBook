using BCrypt.Net;
using QuickBook.Application.Dto.Login;
using QuickBook.Application.Dto.Register;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Users;
using QuickBook.Domain.Interface;
using QuickBook.Helper1;

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

            bool password = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if(!password)
                throw new ArgumentException("Invalid email or password");

            var token = _jwtTokenGenerator.GenerateToken(user, out DateTime expiresAt);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }

        private static RegisterReponsesDto MaptoResponses(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Role = user.Role,
        };
    }
}
