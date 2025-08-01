using RetroGameLog.Domain.Abstractions;

namespace RetroGameLog.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new("User.NotFound", "User not found!");

    public static readonly Error UsernameOverlap = new("User.Overlap", "Username has already exists!");

    public static readonly Error EmailOverlap = new("User.Overlap", "Email has already exists!");
}
