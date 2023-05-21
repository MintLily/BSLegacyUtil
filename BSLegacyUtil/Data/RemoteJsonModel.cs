using Newtonsoft.Json;
using static BSLegacyUtil.Console;

namespace BSLegacyUtil.Data;

// public class BsLegacyVersions {
//     [JsonProperty("Versions")] public List<Versions> Versions { get; set; }
// }
//
// public class Versions {
//     [JsonProperty("Version")] public string Version { get; set; }
//     [JsonProperty("manifestID")] public string ManifestId { get; set; }
//     [JsonProperty("year")] public ushort Year { get; set; }
// }

public class Version {
    [JsonProperty("BSVersion")] public string? BsVersion;
    [JsonProperty("BSManifest")] public string? BsManifest;
    [JsonProperty("ReleaseURL")] public string? ReleaseUrl;
    [JsonProperty("year")] public string? Year;
    [JsonProperty("row")] public int Row;
}

public abstract class RemoteJsonModel {
    private const string JsonUrl = "https://raw.githubusercontent.com/RiskiVR/BSLegacyLauncher/master/Resources/BSVersions.json";
    private static readonly string JsonFile = $"{Vars.BaseDirectory}Versions.json";

    public static List<Version>? BsVersions { get; private set; }// = LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);

    public static void LoadJsonData() {
        var data = "";
        var client = new HttpClient();
        //if (!Vars.IsDebug) 
        try {
            data = client.GetStringAsync(JsonUrl).GetAwaiter().GetResult();
        }
        catch {
            try {
                data = client.GetStringAsync("https://raw.githubusercontent.com/MintLily/BSLegacyUtil/master/Resources/BackupBSVersions.json").GetAwaiter().GetResult();
                /* The contents on this JSON will never be updated */
            }
            catch {
                Error("Failed to load remote JSON data twice. Project is EOL.");
            }
        }
        BsVersions = JsonConvert.DeserializeObject<List<Version>>(data);
        client.Dispose();
    }
    
    public static string GetManifestId(string version) => BsVersions?.FirstOrDefault(x => x.BsVersion == version)?.BsManifest ?? "";
    public static string GetReleaseUrl(string version) => BsVersions?.FirstOrDefault(x => x.BsVersion == version)?.ReleaseUrl ?? "";
    public static string GetYear(string version) => BsVersions?.FirstOrDefault(x => x.BsVersion == version)?.Year ?? "";
    public static int GetRow(string version) => BsVersions?.FirstOrDefault(x => x.BsVersion == version)?.Row ?? 0;
    
    
    // public static BsLegacyVersions Versions { get; set; } = LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);

    // private static BsLegacyVersions LoadJsonData(string file) {
    //     try {
    //         var data = "";
    //         var client = new HttpClient();
    //         if (!Vars.IsDebug) 
    //             data = client.GetStringAsync(file).GetAwaiter().GetResult();
    //         var json = JsonConvert.DeserializeObject<BsLegacyVersions>(Vars.IsDebug ? File.ReadAllText(JsonFile) : data);
    //         client.Dispose();
    //         if (json is null) throw new Exception();
    //         return json;
    //     }
    //     catch (Exception ex) {
    //         Console.Error(ex);
    //         return new BsLegacyVersions();
    //     }
    // }
    //
    // public static string GetManifestFromVersion(string input) {
    //     Versions ??= LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);
    //     var _ = Versions.Versions.FirstOrDefault(v => v.Version == input);
    //     return _ == null ? "" : _.ManifestId;
    // }
    //
    // public static ushort GetYearFromVersion(string input) {
    //     Versions ??= LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);
    //     var _ = Versions.Versions.FirstOrDefault(v => v.Version == input);
    //     return _?.Year ?? 1970;
    // }
}