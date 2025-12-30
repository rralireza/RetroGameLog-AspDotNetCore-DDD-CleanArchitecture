namespace RetroGameLog.Domain.Users;

public sealed class Role
{
    public static readonly Role User = new(1, "User");

    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public ICollection<User> Users { get; init; } = new List<User>();

    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
