using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using Microsoft.VisualBasic.FileIO;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions {
    class Mod {
        public static void modGame(string gameVersion) {
            gamePath = BuildInfo.isWindows ? $"{Environment.CurrentDirectory}\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber";
            string Resources = BuildInfo.isWindows ? "Resources\\" : $"{AppDomain.CurrentDomain.BaseDirectory}Resources";

            if (!Directory.Exists(Resources) || !Directory.Exists(gamePath)) {
                Con.Error("Folder does not exist, cannot mod game.");
                Con.Space();
                BeginInputOption();
            } else {
                switch (gameVersion) {
                    case "0.10.0":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.10.1":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.10.2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.10.2p2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.10.0", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.11.1":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.11.1", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.11.2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.11.2", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
                        Con.LogSuccess("Finished modding game");
                        break;
                    case "0.12.2":
                        FileSystem.CopyDirectory($"{Resources}Beat Saber 0.12.2", gamePath, true);
                        FileSystem.CopyDirectory($"{Resources}CustomSongs", gamePath + "\\CustomSongs", true);
                        FileSystem.CopyDirectory($"{Resources}IPA\\", gamePath, true);
                        Process.Start(gamePath + "\\IPA.exe", gamePath + "\\Beat Saber.exe");
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
