using QuickBook.Application.Dto.Register;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Users;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        private static RegisterReponsesDto MaptoResponses(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Role = user.Role,
        };
    }
}
