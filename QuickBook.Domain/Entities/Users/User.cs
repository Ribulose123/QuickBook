using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Users
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdated { get; private set; }

        private User() { }

        public User(string userName, string email, string passwordHash, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Username is required.", nameof(userName));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password is required.", nameof(passwordHash));

            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdatePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Password is required.", nameof(newPasswordHash));

            PasswordHash = newPasswordHash;
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdateRole(UserRole role)
        {
            Role = role;
            LastUpdated = DateTime.UtcNow;
        }
    }
}