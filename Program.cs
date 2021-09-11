using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using BSLegacyUtil.Utilities;
using BSLegacyUtil.Functions;
using Convert = BSLegacyUtil.Functions.Convert;

namespace BSLegacyUtil
{
    public class BuildInfo
    {
        public const string Name = "BSLegacyUtil";
        public const string Version = "2.1.2";
        public const string Author = "MintLily";
    }

    class Program
    {
        public static bool isDebug;

        internal static string steamUsername, steamPassword, stepInput, versionInput;
        public static string gamePath = string.Empty;
        public static FolderDialog.Bll.FolderDialog.ISelect FolderSelect = new FolderDialog.Bll.FolderDialog.Select();

        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.CommandLine.Contains("debug")) {
                isDebug = true;
                Con.Init();
            }
            Con.BSL_Logo();
            Con.Log("This tool will allow you to easily downgrade your Beat Saber.");
            Con.Log("Brought to you by the", "Beat Saber Legacy Group", ConsoleColor.DarkCyan);
            Con.Space();
            JSONSetup.FixMyMistake();
            JSONSetup.Load();

            if (!isDebug) UpdateCheck.CheckForUpdates();

            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources") || !Directory.Exists(Environment.CurrentDirectory + "\\Depotdownloader"))
            {
                Con.Error("Please be sure you have extracted the files before running this!");
                Con.Error("Program will not run until you have extracted all the contents out of the ZIP file.");
                Con.Exit();
            }
            else BeginInputOption();
        }

        public static void BeginInputOption()
        {
            Con.WriteSeperator();
            Con.Log("Select a step to get started");
            Con.InputOption("1", "\tDownload a version of Beat Saber");
            Con.InputOption("2", "\tInstall to default Steam directory", ConsoleColor.Red, "Option Removed", ConsoleColor.DarkRed);
            Con.InputOption("3", "\tMod current install");
            Con.InputOption("4", "\tConvert Songs");
            Con.InputOption("5", "\tPlay Game");
            Con.Space();
            Con.InputOption("6", "\tExit Program");
            Con.Space();
            Con.Input();
            stepInput = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            switch (stepInput)
            {
                case "1":
                    SelectGameVersion(true);
                    break;
                case "2":
                    Install.InstallGame();
                    break;
                case "3":
                    SelectGameVersion(false);
                    break;
                case "4":
                    Convert.convertSongs();
                    break;
                case "5":
                    PlayGame.Play();
                    break;
                case "6":
                case "c":
                    Process.GetCurrentProcess().Kill();
                    break;
                case "7":
                    if (isDebug) inputSteamLogin();
                    else goto default;
                    break;
                default:
                    Con.Error("Invalid input, please select 1 - 6");
                    BeginInputOption();
                    break;
            }
        }

        public static void SelectGameVersion(bool dlGame)
        {
            Con.Log("Select which version you'd like to use", "- Type \'cancel\' to go back");
            Con.Log("\t0.10.1 \t0.10.2   \t0.10.2p1");
            Con.Log("\t0.11.0 \t0.11.1   \t0.11.2");
            Con.Log("\t0.12.0 \t0.12.0p1 \t0.12.1   \t0.12.2");
            Con.Log("\t0.13.0 \t0.13.0p1 \t0.13.1   \t0.13.2");
            Con.Log("\t1.0.0  \t1.0.1");
            Con.Log("\t1.1.0  \t1.1.0p1");
            Con.Log("\t1.2.0");
            Con.Log("\t1.3.0");
            Con.Log("\t1.4.0  \t1.4.2");
            Con.Log("\t1.5.0");
            Con.Log("\t1.6.0  \t1.6.1");
            Con.Log("\t1.7.0");
            Con.Log("\t1.8.0");
            Con.Log("\t1.9.0  \t1.9.1");
            Con.Log("\t1.10.0");
            Con.Log("\t1.11.0 \t1.11.1");
            Con.Log("\t1.12.1 \t1.12.2");
            Con.Log("\t1.13.0 \t1.13.2   \t1.13.4   \t1.13.5");
            Con.Log("\t1.14.0");
            Con.Log("\t1.15.0");
            Con.Log("\t1.16.0 \t1.16.1 \t1.16.2 \t1.16.3\t1.16.4");
            Con.Log("\t1.17.0 \t1.17.1");
            Con.Input();
            versionInput = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (versionInput == "cancel" || versionInput == "CANCEL" || versionInput == "Cancel" || versionInput == "c")
                BeginInputOption();

            if (dlGame)
                Download.DLGame(versionInput);
            else
                Mod.modGame(versionInput);
        }

        public static void inputSteamLogin()
        {
            Con.Log("Steam Username", "(not display name)");
            Con.SteamUN();
            steamUsername = Console.ReadLine();
            Con.Log("Steam Password");//, "(press enter once before you start typing)");
            Con.SteamPW();
            steamPassword = Console.ReadLine();

            if (isDebug) {
                Con.Log("password is: ", steamPassword);
                BeginInputOption();
            }
        }
    }
}
