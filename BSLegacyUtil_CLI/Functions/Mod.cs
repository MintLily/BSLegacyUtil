using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using Microsoft.VisualBasic.FileIO;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions {
    public class Mod {
        public static void ModGame(string gameVersion) {
            _gamePath = BuildInfo.IsWindows ? $"{Environment.CurrentDirectory}\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber";
            string Resources = BuildInfo.IsWindows ? "Resources\\" : $"{AppDomain.CurrentDomain.BaseDirectory}Resources";

            if (!Directory.Exists(Resources) || !Directory.Exists(_gamePath)) {
                Con.Error("Folder does not exist, cannot mod game.");
                Con.Space();
                BeginInputOption();
            } else {
                switch (gameVersion) {
                    case "0.10.0":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.10.1":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.10.2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.10.2p2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.11.1":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.11.1", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.11.2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.11.2", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.12.2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.12.2", _gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", _gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", _gamePath, true);
                        Process.Start(_gamePath + "\\IPA.exe", _gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    default:
                        Process.Start("cmd", "/C start https://github.com/Assistant/ModAssistant");
                        Con.Log("If you need any help, join the Beat Saber Legacy Group discord.");
                        Con.Log("Find more information on our website:", "https://bslegacy.com", ConsoleColor.Green);
                        Con.Space();
                        Con.Log("Install extra plugins here:", "https://bslegacy.com/plugins", ConsoleColor.Green);
                        Con.Space();
                        Con.Log("\t\t - RiskiVR (Risk#3904)");
                        break;
                }
                Con.Space();
                BeginInputOption();
            }
        }
    }
}
