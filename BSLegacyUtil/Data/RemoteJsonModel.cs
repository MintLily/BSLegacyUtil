using Newtonsoft.Json;

namespace BSLegacyUtil.Data;

public class BsLegacyVersions {
    [JsonProperty("Versions")] public List<Versions> Versions { get; set; }
}

public class Versions {
    [JsonProperty("Version")] public string Version { get; set; }
    [JsonProperty("manifestID")] public string ManifestId { get; set; }
    [JsonProperty("year")] public ushort Year { get; set; }
}

public abstract class RemoteJsonModel {
    private const string JsonUrl = "https://raw.githubusercontent.com/MintLily/BSLegacyUtil/main/Resources/Versions.json";
    private static readonly string JsonFile = $"{Vars.BaseDirectory}Versions.json";
    public static BsLegacyVersions Versions { get; set; } = LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);

    private static BsLegacyVersions LoadJsonData(string file) {
        try {
            var data = "";
            var client = new HttpClient();
            if (!Vars.IsDebug) 
                data = client.GetStringAsync(file).GetAwaiter().GetResult();
            var json = JsonConvert.DeserializeObject<BsLegacyVersions>(Vars.IsDebug ? File.ReadAllText(JsonFile) : data);
            client.Dispose();
            if (json is null) throw new Exception();
            return json;
        }
        catch (Exception ex) {
            Console.Error(ex);
            return new BsLegacyVersions();
        }
    }
    
    public static string GetManifestFromVersion(string input) {
        Versions ??= LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);
        var _ = Versions.Versions.FirstOrDefault(v => v.Version == input);
        return _ == null ? "" : _.ManifestId;
    }

    public static ushort GetYearFromVersion(string input) {
        Versions ??= LoadJsonData(Vars.IsDebug ? JsonFile : JsonUrl);
        var _ = Versions.Versions.FirstOrDefault(v => v.Version == input);
        return _?.Year ?? 1970;
    }
}