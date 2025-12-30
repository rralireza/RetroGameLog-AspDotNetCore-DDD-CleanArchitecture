namespace RetroGameLog.Domain.Users;

public sealed class Permission
{

    public static readonly Permission UserCanRead = new(1, "User.Read");
    public static readonly Permission UserCanReadAndWrite = new(2, "User.ReadAndWrite");

    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public ICollection<Role> Roles { get; init; } = new List<Role>();
}
