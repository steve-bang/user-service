
namespace Steve.ManagerHero.UserService.Domain.Entities;

public class PasswordHistoryEntity : Entity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string Salt { get; private set; } = default!;
    public DateTime ChangedAt { get; private set; }

    private PasswordHistoryEntity()
    {
        Id = CreateNewId();
    }

    public PasswordHistoryEntity(Guid userId, string passwordHash, string salt, DateTime changedAt) : this()
    {
        UserId = userId;
        PasswordHash = passwordHash;
        Salt = salt;
        ChangedAt = changedAt;
    }

    public PasswordHistoryEntity(Guid userId, string passwordHash, string salt) : this(userId, passwordHash, salt, DateTime.UtcNow)
    {
    }
}