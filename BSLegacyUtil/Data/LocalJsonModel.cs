using Newtonsoft.Json;

namespace BSLegacyUtil.Data;

public class Config {
    public string? RememberedVersion { get; set; }
    public string? RememberedSteamUserName { get; set; }
}

public abstract class LocalJsonModel {
    public static Config? TheConfig { get; private set; }
    
    public static void Start() {
        if (!File.Exists($"{Vars.BaseDirectory}Config.json")) {
            var c = new Config { RememberedVersion = "1.0.0", RememberedSteamUserName = ""};
            File.WriteAllText($"{Vars.BaseDirectory}Config.json", JsonConvert.SerializeObject(c, Formatting.Indented));
        }
        
        TheConfig = JsonConvert.DeserializeObject<Config>(File.ReadAllText($"{Vars.BaseDirectory}Config.json"));
    }
    
    public static void Save() => File.WriteAllText($"{Vars.BaseDirectory}Config.json", JsonConvert.SerializeObject(TheConfig, Formatting.Indented));
}