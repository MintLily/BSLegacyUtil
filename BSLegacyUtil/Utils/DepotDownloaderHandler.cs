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
    
    private static ProcessStartInfo _ddInfo = new () {
        // FileName =  Path.Combine(Vars.BaseDirectory, "DepotDownloader", "DepotDownloader.exe"),
        FileName = "dotnet",
        WorkingDirectory = Vars.BaseDirectory,
        Arguments = $"\"{Path.Combine(Vars.BaseDirectory, "DepotDownloader", "DepotDownloader.dll")}\" " +
            $"-username \"{LocalJsonModel.TheConfig!.RememberedSteamUserName}\" -password \"{Vars.SteamPassword.Replace("\"", "\\\"")}\" " +
            $"-manifest {RemoteJsonModel.GetManifestId(LocalJsonModel.TheConfig.RememberedVersion!)} -depot {Vars.GameDeoptId} -app {Vars.GameAppId} " +
            $"-dir \"{Path.Combine(Vars.BaseDirectory, "Installed Versions", $"Beat Saber {LocalJsonModel.TheConfig.RememberedVersion}")}\" -validate",
        WindowStyle = ProcessWindowStyle.Hidden,
        RedirectStandardOutput = true,
        RedirectStandardInput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };
    
    public static void StartDownload() {
        restart:
        Console.Log("Game Version: " + $"{LocalJsonModel.TheConfig!.RememberedVersion}".Pastel("#3498DB"));
        Console.Log("Manifest ID: " + $"{RemoteJsonModel.GetManifestId(LocalJsonModel.TheConfig.RememberedVersion!)}".Pastel("#3498DB"));
        Console.Log("Year: " + $"{RemoteJsonModel.GetYear(LocalJsonModel.TheConfig.RememberedVersion!)}".Pastel("#3498DB"));
        Console.Log("Release URL: " + RemoteJsonModel.GetReleaseUrl(LocalJsonModel.TheConfig.RememberedVersion!).Pastel("#3498DB"));
        Console.Space();
        Console.Log("===== STEAM LOGIN =====".Pastel("#FFD700"));
        if (string.IsNullOrWhiteSpace(LocalJsonModel.TheConfig.RememberedSteamUserName)) {
            System.Console.Write("Username".Pastel("#00FF00") + ": ");
            LocalJsonModel.TheConfig.RememberedSteamUserName = System.Console.ReadLine();
            LocalJsonModel.Save();
        } else 
            Console.Log("Username: " + LocalJsonModel.TheConfig.RememberedSteamUserName.Pastel("#3498DB"));

        if (string.IsNullOrWhiteSpace(Vars.SteamPassword)) {
            System.Console.Write("Password".Pastel("#00FF00") + ": ");
            var steamPassword = System.Console.ReadLine();
            if (string.IsNullOrWhiteSpace(steamPassword)) {
                Console.Warning("Password cannot be empty!");
                goto restart;
            }
            Vars.SteamPassword = steamPassword;
        } else
            Console.Log("Password: " + "********".Pastel("#3498DB"));

        var process = Process.Start("dotnet", _ddInfo.Arguments);
        process.WaitForExit();
        
        Console.Space();
        Console.Log("Would you like to continue? (y/n)");
        var @continue = System.Console.ReadLine();
        if (@continue!.ToLower().Contains('y'))
            Program.Start();
        else
            Environment.Exit(0);

        /*using var process = Process.Start(_ddInfo);
        var lines = 0;
        if (process == null) {
            Console.Error("Failed to start DepotDownloader!");
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }
        var errors = process!.StandardError.ReadToEnd();
        var output = process.StandardOutput.ReadToEnd();
        
        Console.Log(output);
        if (output.EndsWith(Environment.NewLine) || output.Contains(" code ")) {
            PeekAtLine(output, process);
            lines++;
        }

        // Console.Log(output);
        Console.Log(errors);
            
        if (lines > 0) return;
        _steamLoginResponse = SteamLoginResponse.NETNOTINSTALLED;
        Console.Warning("You don't have .NET installed! Opening Microsoft's webpage to download it... ");
        if (Vars.IsWindows) {
            if (!Vars.IsDebug)
                Process.Start("cmd", "/c start https://link.bslegacy.com/dotnet7");
            return;
        }
        // elseif isLinux
        Process.Start("https://dotnet.microsoft.com/en-us/download/dotnet/7.0");
        Program.Start();
    }

    private static void PeekAtLine(string line, Process? process) {
        Console.Log(line);
        if (line.Contains("This account is protected") || line.ToLower().Contains("guard")) {
            _steamLoginResponse = SteamLoginResponse.STEAMGUARD;
            System.Console.Write("SteamGuard/2FA".Pastel("#00FF00") + ": ");
            var steamGuardCode = System.Console.ReadLine();
            if (string.IsNullOrWhiteSpace(steamGuardCode)) {
                Console.Warning("SteamGuard/2FA cannot be empty!");
                process!.Close();
                StartDownload();
            }
            process!.StandardInput.WriteLine(steamGuardCode);
        }

        if (line.ContainsMultiple("requires a username and password to be set", "Unset")) {
            _steamLoginResponse = SteamLoginResponse.PASSWORDUNSET;
            Console.Warning("Password cannot be empty, try again!");
            process!.Close();
            Console.Space();
            StartDownload();
        }

        if (line.Contains("InvalidPassword")) {
            _steamLoginResponse = SteamLoginResponse.INVALIDPASSWORD;
            Console.Warning("Invalid password, try again!");
            process!.Close();
            Console.Space();
            StartDownload();
        }

        if (line.ContainsMultiple("404 for depot manifest", "App", "is not available from this account")) {
            _steamLoginResponse = SteamLoginResponse.BEATSABERNOTOWNED;
            Console.Warning("Beat Saber is not owned by this account!");
            process!.Close();
            Console.Space();
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }

        if (line.Contains("401 for depot manifest")) {
            _steamLoginResponse = SteamLoginResponse.UNAUTHORIZED;
            Console.Warning("Unauthorized!");
            process!.Close();
            Console.Space();
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }
        
        if (line.Contains("Connection to Steam failed")) {
            _steamLoginResponse = SteamLoginResponse.CONNECTIONFAILED;
            try { process!.Kill(); }
            catch (Exception ex) { Console.Error(ex); }
            Console.Warning("Connection to Steam failed");
            Console.Space();
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }

        if (line.Contains("RateLimitExceeded")) {
            _steamLoginResponse = SteamLoginResponse.RATELIMIT;
            Console.Warning("Rate limit exceeded, please try again in 30 minutes");
            process!.Close();
            Console.Space();
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }
        
        if (line.Contains("InvalidLoginAuthCode")) {
            _steamLoginResponse = SteamLoginResponse.INVALIDLOGINAUTHCODE;
            Console.Warning("Invalid login auth code, please try again in 30 seconds");
            process!.Close();
            Console.Space();
            StartDownload();
        }

        if (line.Contains("ExpiredLoginAuthCode")) {
            _steamLoginResponse = SteamLoginResponse.EXPIREDLOGINAUTHCODE;
            Console.Warning("Expired login auth code, please try again in 30 seconds");
            process!.Close();
            Console.Space();
            StartDownload();
        }

        if (line.Contains("There is not enough space")) {
            _steamLoginResponse = SteamLoginResponse.NOTENOUGHSPACE;
            Console.Warning("There is not enough space on the disk");
            process!.Close();
            Console.Space();
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }

        if (line.Contains("Got session token")) {
            Console.Log("Successfully logged in!");
            Console.Log("One moment while we download Beat Saber v" + LocalJsonModel.TheConfig!.RememberedVersion.Pastel("#3498DB") + " ...");
        }
        
        if (line.Contains("Total downloaded:")) {
            Console.Log("Download Finished!");
            process!.Close();
            Console.Space();
            Console.Log("Press enter to restart BSLegacyUtil...");
            System.Console.Read();
            Program.Start();
        }
        
        if (line.Contains('%')) {
            var percentage = float.Parse(line.Split("%")[0]);
            var finalPercentage = $"{percentage:0.0}";
            Console.Log("Download Progress: " + finalPercentage.Pastel("#3498DB") + "%");
            if (finalPercentage.Contains("32.4")) 
                Console.Warning("This may take a while, please be patient");
        }

        if (!line.Contains("Access to the path is denied")) return;
        _steamLoginResponse = SteamLoginResponse.PATHDENIED;
        Console.Warning("Access to the path is denied, try moving the BSLegacyUtil folder to another location");
        process!.Close();
        Console.Space();
        Console.Log("Press enter to restart BSLegacyUtil...");
        System.Console.Read();
        Program.Start();*/
    }
}