using QuickBook.Application.Dto.Login;
using QuickBook.Application.Dto.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface IAuthService
    {
        Task<RegisterReponsesDto> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
