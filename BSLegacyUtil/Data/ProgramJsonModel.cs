/*using Newtonsoft.Json;

namespace BSLegacyUtil.Data;

public class ProgramData {
    [JsonProperty("Expected Version")] public string? ExpectedVersion { get; set; }
    [JsonProperty("Expected SHA256")] public string? ExpectedSha256 { get; set; }
    public List<OldProgramData>? OldProgramData { get; set; }
}

public class OldProgramData {
    public string? ExpectedVersion { get; set; }
    public string? ExpectedSha256 { get; set; }
}

public abstract class ProgramJsonModel {
    private const string JsonUrl = "https://raw.githubusercontent.com/BeatSaberLegacyGroup/BSLegacyUtil/main/Resources/ProgramData.json";
    // private static readonly string JsonFile = $"{Vars.BaseDirectory}ProgramData.json";

    public static ProgramData TheProgramData { get; } = GetProgramData(JsonUrl);//Vars.IsDebug ? JsonFile : JsonUrl);
    
    private static ProgramData GetProgramData(string file) {
        try {
            var data = "";
            var client = new HttpClient();
            /*if (!Vars.IsDebug) #1#
                data = client.GetStringAsync(file).GetAwaiter().GetResult();
                var json = JsonConvert.DeserializeObject<ProgramData>(data);//Vars.IsDebug ? File.ReadAllText(JsonFile) : data);
            client.Dispose();
            if (json is null) throw new Exception();
            return json;
        }
        catch (Exception ex) {
            Console.Error(ex);
            return new ProgramData();
        }
    }
}*/