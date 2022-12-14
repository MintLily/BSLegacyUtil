using System.Data.Common;
using System.Diagnostics;
using BSLegacyUtil.Data;
using Pastel;

namespace BSLegacyUtil.Utils; 

public class DepotDownloaderHandler {
    private enum SteamLoginResponse { // Yoinked form Riski's code
        NONE,
        TWOFACTOR,
        STEAMGUARD,
        INVALIDPASSWORD,
        PASSWORDUNSET,
        RATELIMIT,
        EXCEPTION,
        INVALIDLOGINAUTHCODE,
        EXPIREDLOGINAUTHCODE,
        BEATSABERNOTOWNED,
        CONNECTIONFAILED,
        NETNOTINSTALLED,
        NOTENOUGHSPACE,
        PREALLOCATING,
        PATHDENIED,
        UNAUTHORIZED
    }
    
    private static SteamLoginResponse _steamLoginResponse = SteamLoginResponse.NONE;
    
    private static ProcessStartInfo ddInfo = new () {
        FileName = Vars.BaseDirectory + "Resources\\DepotDownloader\\DepotDownloader.exe",
        Arguments = $"-username \"{LocalJsonModel.TheConfig!.RememberedSteamUserName}\" -password \"{Vars.SteamPassword.Replace("\"", "\\\"")}\" " +
                    $"-manifest {RemoteJsonModel.GetManifestFromVersion(LocalJsonModel.TheConfig.RememberedVersion!)} -depot {Vars.GameDeoptId} -app {Vars.GameAppId} " +
                    $"-dir \"{Path.Combine(Vars.BaseDirectory, "Installed Versions", $"Beat Saber {LocalJsonModel.TheConfig.RememberedVersion}")}\" -validate -remember-password",
        RedirectStandardOutput = true,
        RedirectStandardInput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };
    
    private static Thread? _ddThread;
    private static Process? _ddProcess;
    
    public static void StartDownload() {
        restart:
        Console.Log($"Game Version: {LocalJsonModel.TheConfig!.RememberedVersion} => [{RemoteJsonModel.GetManifestFromVersion(LocalJsonModel.TheConfig.RememberedVersion!)}] from year {RemoteJsonModel.GetYearFromVersion(LocalJsonModel.TheConfig.RememberedVersion!)}");
        Console.Space();
        Console.Log("=== STEAM LOGIN ===");
        if (string.IsNullOrWhiteSpace(LocalJsonModel.TheConfig!.RememberedSteamUserName)) {
            System.Console.Write("Username: ".Pastel("#00FF00"));
            LocalJsonModel.TheConfig!.RememberedSteamUserName = System.Console.ReadLine();
        }
        else 
            Console.Log("Username: " + LocalJsonModel.TheConfig!.RememberedSteamUserName);
        
        System.Console.Write("Password: ".Pastel("#00FF00"));
        var steamPassword = System.Console.ReadLine();
        if (string.IsNullOrWhiteSpace(steamPassword)) {
            Console.Warning("Password cannot be empty!");
            goto restart;
        }
        Vars.SteamPassword = steamPassword;
        LocalJsonModel.Save();
        
        _ddThread = new Thread(() => {
            try {
                var line = "";
                var lines = 0;
                _ddProcess = Process.Start(ddInfo);
                while (!_ddProcess!.StandardOutput.EndOfStream && !_ddProcess.HasExited) {
                    if (line.EndsWith(Environment.NewLine) || line.Contains(" code ")) {
                        PeekAtLine(line);
                        lines++;
                    }

                    if (lines > 0) continue;
                    _steamLoginResponse = SteamLoginResponse.NETNOTINSTALLED;
                    Console.Warning("You don't have .NET installed! Opening Microsoft's webpage to download it...");
                    if (Vars.IsWindows) {
                        Process.Start("cmd", "/c start https://cdn.bslegacy.com/dotNET-7");
                        return;
                    }
                    // elseif isLinux
                    Process.Start("https://dotnet.microsoft.com/en-us/download/dotnet/7.0");
                }
            }
            catch (Exception ex) {
                _steamLoginResponse = SteamLoginResponse.EXCEPTION;
                Console.Error($"[DepotDownloader.StartDownload] {ex.Message}");
            }
        });
        _ddThread.Start();
        Program.BeginInput();
    }

    private static void PeekAtLine(string line) {
        if (line.Contains("This account is protected")) {
            _steamLoginResponse = SteamLoginResponse.STEAMGUARD;
            return;
        }

        if (line.ContainsMultiple("requires a username and password to be set", "Unset")) {
            _steamLoginResponse = SteamLoginResponse.PASSWORDUNSET;
            return;
        }

        if (line.Contains("InvalidPassword")) {
            _steamLoginResponse = SteamLoginResponse.INVALIDPASSWORD;
            return;
        }

        if (line.ContainsMultiple("404 for depot manifest", "App", "is not available from this account")) {
            _steamLoginResponse = SteamLoginResponse.BEATSABERNOTOWNED;
            return;
        }

        if (line.Contains("401 for depot manifest")) {
            _steamLoginResponse = SteamLoginResponse.UNAUTHORIZED;
            return;
        }

        // if (line.Contains("Got depot key"))
        //     _steamLoginResponse = 
        
        if (line.Contains("Connection to Steam failed")) {
            _steamLoginResponse = SteamLoginResponse.CONNECTIONFAILED;
            try { _ddProcess.Kill(); }
            catch (Exception ex) { Console.Error(ex); }
            Console.Warning("Connection to Steam failed");
            return;
        }

        if (line.Contains("RateLimitExceeded")) {
            _steamLoginResponse = SteamLoginResponse.RATELIMIT;
            Console.Warning("Rate limit exceeded, please try again in 30 minutes");
            return;
        }
        
        if (line.Contains("InvalidLoginAuthCode")) {
            _steamLoginResponse = SteamLoginResponse.INVALIDLOGINAUTHCODE;
            Console.Warning("Invalid login auth code, please try again in 30 seconds");
            return;
        }

        if (line.Contains("ExpiredLoginAuthCode")) {
            _steamLoginResponse = SteamLoginResponse.EXPIREDLOGINAUTHCODE;
            Console.Warning("Expired login auth code, please try again in 30 seconds");
            return;
        }

        if (line.Contains("There is not enough space")) {
            _steamLoginResponse = SteamLoginResponse.NOTENOUGHSPACE;
            Console.Warning("There is not enough space on the disk");
            return;
        }

        if (!line.Contains("Access to the path is denied")) return;
        _steamLoginResponse = SteamLoginResponse.PATHDENIED;
        Console.Warning("Access to the path is denied");
    }
}