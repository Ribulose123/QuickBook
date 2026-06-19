using QuickBook.Domain.Entities.Users;

namespace QuickBook.Helper1
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, out DateTime expiresAt);
    }
}
