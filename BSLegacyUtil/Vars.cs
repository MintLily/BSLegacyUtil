namespace BSLegacyUtil; 

public static class Vars {
    public const string Name = "BSLegacyUtil";
    public const string Version = "3.2.0.0";
    internal static string InternalSha256 = "0x0";
    internal static bool FileIntegrityFailed = true;
    public const string Author = "MintLily";
    public static bool IsWindows;
    public static readonly Version TargetDotNetVer = new("7.0.0");
#if DEBUG
    public static bool IsDebug = true;
#else
    public static bool IsDebug { get; set; }
#endif
    public static string BaseDirectory { get; internal set; } = IsWindows ? Environment.CurrentDirectory + Path.PathSeparator : AppDomain.CurrentDomain.BaseDirectory;
    
    /*===============================================*/
    public static string SteamPassword { get; internal set; } = "";
    // public static string SteamGuardCode { get; internal set; } = "";
    public static string GameManifestId { get; internal set; } = "";
    public const string GameDeoptId = "620981";
    public const string GameAppId = "620980";
}