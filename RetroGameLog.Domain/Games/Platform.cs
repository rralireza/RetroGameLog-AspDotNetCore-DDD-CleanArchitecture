namespace RetroGameLog.Domain.Games;

public sealed record Platform
{
    internal static readonly Platform Unknown = new("Unknown");
    public static readonly Platform Playstation = new("Playstation");
    public static readonly Platform Playstation2 = new("Playstation 2");
    public static readonly Platform Playstation3 = new("Playstation 3");
    public static readonly Platform Playstation4 = new("Playstation 4");
    public static readonly Platform Playstation5 = new("Playstation 5");
    public static readonly Platform Xbox = new("Xbox");
    public static readonly Platform Xbox360 = new("Xbox 360");
    public static readonly Platform XboxOne = new("Xbox One");
    public static readonly Platform XboxSeriesX = new("Xbox Series X");
    public static readonly Platform XboxSeriesS = new("Xbox Series S");
    public static readonly Platform NintendoSwitch = new("Nintendo Switch");
    public static readonly Platform NintendoSwitch2 = new("Nintendo Switch 2");
    public static readonly Platform NintendoWii = new("Nintendo Wii");
    public static readonly Platform PC = new("PC");

    private Platform(string platformName) => PlatformName = platformName;

    public string PlatformName { get; init; }

    public static Platform FindPlatform(string platformName) =>
        AllPlatforms.FirstOrDefault(x => x.PlatformName == platformName) ?? throw new ApplicationException("Invalid platform name!");

    public static readonly IReadOnlyCollection<Platform> AllPlatforms = new List<Platform>
    {
        Unknown,
        Playstation,
        Playstation2,
        Playstation3,
        Playstation4,
        Playstation5,
        Xbox,
        Xbox360,
        XboxOne,
        XboxSeriesX,
        XboxSeriesS,
        NintendoSwitch,
        NintendoSwitch2,
        NintendoWii,
        PC
    }.AsReadOnly();
}
