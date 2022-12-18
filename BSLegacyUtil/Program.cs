﻿using System.Diagnostics;
using System.Text;
using BSLegacyUtil.Data;
using BSLegacyUtil.Utils;
using Pastel;
using static BSLegacyUtil.Console;

namespace BSLegacyUtil;

public abstract class Program {

    [STAThread]
    private static void Main(string[] args) {
        LocalJsonModel.Start();
        var bsDir = Path.Combine(Vars.BaseDirectory, "Installed Versions", $"Beat Saber {LocalJsonModel.TheConfig!.RememberedVersion}");
        if ((Environment.CommandLine.Length >= 1 || args.Length >= 1) && Environment.CommandLine.Contains("--autostart")) {
            if (Directory.Exists(bsDir)) {
                PlayGame(Environment.CommandLine.Contains("--oculus"));
                Task.Delay(1000);
                Process.GetCurrentProcess().Kill();
            }
            Warning("Game Not installed, running program like normal.");
        }
        
        Vars.IsWindows = Environment.OSVersion.ToString().ToLower().Contains("windows");
        Vars.IsDebug = Environment.CommandLine.Contains("--debug");
        
        BsLegacyAscii();
        Log("This tool will allow you to easily downgrade your Beat Saber.");
        Log("Brought to you by the " + "Beat Saber Legacy Group".Pastel("#3498DB"));
        Space();
        
        // TODO: Include auto updater (maybe)
        if (!Vars.IsDebug) {
            var update = new HttpClient();
            update.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0");
            var str = update.GetStringAsync("https://api.github.com/repos/MintLily/BSLegacyUtil/releases").GetAwaiter().GetResult();
            update.Dispose();
            
            if (!str.Contains($"\"tag_name\": \"{Vars.Version}\"")) {
                WriteSeparator(ConsoleColor.Red);
                CenterLog($"A newer version of {Vars.Name} is available!".Pastel("#ff0000"));
                WriteSeparator(ConsoleColor.Red);
                Space();
                Process.Start(Vars.IsWindows ? "cmd.exe" : "https://github.com/MintLily/BSLegacyUtil/releases",
                    Vars.IsWindows ? "/c start https://github.com/MintLily/BSLegacyUtil/releases" : "");
            }
        }
        
        var http = new HttpClient();
        http.DefaultRequestHeaders.Add("User-Agent", Vars.Name);
        var contents = http.GetStringAsync("https://bslegacy.com/assets/motd.txt").GetAwaiter().GetResult();
        CenterLog(contents.Replace("<br>", "\n"));
        Space();
        http.Dispose();
        
        var showError = !Directory.Exists($"{Vars.BaseDirectory}Resources") || !Directory.Exists($"{Vars.BaseDirectory}DepotDownloader");
        
        if (showError) {
            Error("Please be sure you have extracted the files before running this!");
            Error("Program will not run until you have extracted all the contents out of the ZIP file.");
            System.Console.ReadLine();
            Environment.Exit(0);
        }
        
        WriteSeparator();
        
        var sys = Environment.Version; // Gets Current Installed version
        var tar = Vars.TargetDotNetVer; // Gets Targeted version

        if (!(sys.Major == tar.Major && sys.Minor == tar.Minor && sys.Build >= tar.Build)) {
            if (Vars.IsWindows) {
                Warning("Make sure you have the required packages installed on your machine\n" +
                                ".NET Desktop Runtime v6.0.0+: https://link.bslegacy.com/dotNET-7 \n" +
                                "This MUST be installed in order to use this app properly.\n\n" +
                                "If you already have just installed these, Press \"OK\" and ignore this message.");
                System.Console.ReadLine();
                Environment.Exit(0);
            }
            else {
                try {
                    var s = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}_INSTALL THIS FIRST.txt");
                    foreach (var s1 in s) {
                        Log(s1);
                    }
                }
                catch {
                    Error("_INSTALL THIS FIRST.txt is null");
                    Space();
                    Log("Follow the instructions on the opened webpage: https://bslegacy.com/dl/CA-Utility/LinuxInstallGuide");
                    Process.Start("https://bslegacy.com/dl/CA-Utility/LinuxInstallGuide");
                    System.Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }
        
        BeginInput();
    }

    public static void BeginInput() {
        beginInput:
        Space();
        Log("\tSelect an option below: \t\t Current Version Selected: " + LocalJsonModel.TheConfig.RememberedVersion.Pastel("#3498DB") + " - Steam Account: " + $"{(string.IsNullOrWhiteSpace(LocalJsonModel.TheConfig.RememberedSteamUserName) ? "{{ UNSET }}" : LocalJsonModel.TheConfig.RememberedSteamUserName)}".Pastel("#3498DB"));
        Log("\t1".Pastel("#E37640") + "   Select Game Version");
        Log("\t2".Pastel("#E37640") + "   Download selected version");
        Log("\t3".Pastel("#E37640") + "   Mod selected version");
        Log("\t4".Pastel("#E37640") + "   Play Game (On Steam in Normal Mode)");
        Log("\t5".Pastel("#E37640") + "   Play Game (On Steam in Oculus Mode)");
        Space();
        Log("\t6".Pastel("#E37640") + "   Exit Program");
        Space();
        var input = System.Console.ReadLine();
        Space();

        switch (input) {
            case "1":
                // Select Game Version
                Log("Select which version you'd like to use " + "- Type \'cancel\' to go back".Pastel("#DA4A80"));

                Dictionary<ushort, StringBuilder> sb = new();
                string lastMajor = "Elly is cute!~", lastMinor = "I love you Elly~! <3"; // Can you tell I really love her?

                foreach (var v in RemoteJsonModel.Versions.Versions) {
                    var s = $"  {v.Version}";
                    var tVer = v.Version.Split('.');

                    if (tVer.Length != 3)
                        continue;

                    if (!sb.ContainsKey(v.Year)) {
                        lastMajor = tVer[0];
                        lastMinor = tVer[1];
                        sb.Add(v.Year, new StringBuilder(">\n\t"));
                    }

                    if (tVer[0] != lastMajor || tVer[1] != lastMinor) {
                        lastMajor = tVer[0];
                        lastMinor = tVer[1];
                        sb[v.Year].Append($"\n\t{s}"); // AppendLine was borked
                    } else
                        sb[v.Year].Append(s);
                }
                
                Space();
                foreach (var stringBuilder in sb)
                    Log(stringBuilder.ToString().Replace("[", "").Replace(",", "").Replace("]", ""));
                Space();
                
                var versionInput = System.Console.ReadLine();
                LocalJsonModel.TheConfig.RememberedVersion = versionInput.Contains('c') || versionInput.Contains("cancel") ? LocalJsonModel.TheConfig.RememberedVersion : versionInput;
                LocalJsonModel.Save();
                goto beginInput;
            case "2":
                // Download Version of Game
                DepotDownloaderHandler.StartDownload();
                break;
            case "3":
                // Mod Version of Game
                var nonMaVersions = new List<string> { "0.10.1", "0.10.2", "0.10.2p1", "0.11.0", "0.11.1", "0.11.2", "0.12.0",
                    "0.12.0p1", "0.12.1", "0.12.2", "0.13.0", "0.13.0p1", "0.13.1" };
                var bsDir = Path.Combine(Vars.BaseDirectory, "Installed Versions", $"Beat Saber {LocalJsonModel.TheConfig.RememberedVersion}");
                var isOldSelected = !nonMaVersions.Any(x => x.Equals(LocalJsonModel.TheConfig.RememberedVersion));
                try {
                    CustomDirectory.DirectoryCopy($"{Vars.BaseDirectory}Resources\\BSIPA-{(isOldSelected ? "Legacy" : "4.2.2")}", bsDir, true);
                    Log($"IPA {(isOldSelected ? "Legacy" : "4.2.2")} successfully installed!");
                }
                catch (Exception ex) {
                    Error(ex);
                    goto beginInput;
                }
                
                Process.Start(new ProcessStartInfo {
                    WorkingDirectory = bsDir,
                    FileName = $"{bsDir}{Path.PathSeparator}IPA.exe"
                });
                
                if (!isOldSelected) {
                    var tempInput = System.Console.ReadLine();
                    System.Console.Write("Open Mod Assistant? (Y/N) ");
                    if (tempInput.Contains('y')) 
                        Process.Start($"{Vars.BaseDirectory}ModAssistant.exe");
                }
                goto beginInput;
            case "4":
                // Play Game (Normal Steam Mode)
                PlayGame();
                break;
            case "5":
                // Play Game (Oculus Mode)
                PlayGame(true);
                break;
            case "c":
            case "x":
            case "0":
            case "6":
                // Exit Program
                Environment.Exit(0);
                break;
            case "reboot":
                System.Console.Clear();
                Main(null);
                break;
            default:
                Error("Invalid input, please select 1 - 6");
                goto beginInput;
        }
    }

    private static void PlayGame(bool oculus = false) {
        var p = new Process();
        var temp = Path.Combine(Vars.BaseDirectory, "Installed Versions", $"Beat Saber {LocalJsonModel.TheConfig!.RememberedVersion}");
        p.StartInfo = new ProcessStartInfo($"{temp}{Path.PathSeparator}Beat Saber.exe", oculus ? "-vrmode oculus" : "") {
            UseShellExecute = false,
            WorkingDirectory = temp
        };

        try {
            p.StartInfo.Environment["SteamAppId"] = Vars.GameAppId;
            p.Start();
        }
        catch (Exception e) {
            Error("Failed to start game, please try again.");
            Error(e.Message);
        }
    }
}