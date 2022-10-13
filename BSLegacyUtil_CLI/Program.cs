using BSLegacyUtil.Functions;
using BSLegacyUtil.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert = BSLegacyUtil.Functions.Convert;

namespace BSLegacyUtil {
    public class BuildInfo {
        public const string Name = "BSLegacyUtil";
        public const string Version = "2.12.1";
        public const string Author = "MintLily";
        public static bool IsWindows; // Linux will be maintained by EllyVR, but I will keep this here to help her just a bit.

        //public static Version DepotDLTargetDotNETVer = new("5.0.10");
        public static Version TargetDotNetVer = new("6.0.0");
    }

    internal class Program {
        public static bool IsDebug;

        internal static string _steamUsername, _steamPassword;
        private static string _stepInput, _versionInput;
        public static string _gamePath = string.Empty;
        public static FolderDialog.Bll.FolderDialog.ISelect FolderSelect = new FolderDialog.Bll.FolderDialog.Select();

        [STAThread]
        static void Main(string[] args) {
            BuildInfo.IsWindows = Environment.OSVersion.ToString().ToLower().Contains("windows");

            if ((Environment.CommandLine.Length >= 1 || args.Length >= 1) && Environment.CommandLine.Contains("--autostart")) {
                if (Directory.Exists(BuildInfo.IsWindows ? $"{Environment.CurrentDirectory}\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber")) {
                    PlayGame.LaunchGame(Environment.CommandLine.Contains("oculus"));
                    Task.Delay(1000);
                    Process.GetCurrentProcess().Kill();
                }
                Con.Log("Game Not installed, running program like normal.");
            }

            if (Environment.CommandLine.Contains("debug")) {
                IsDebug = true;
                Con.Init();
            }
            Con.BSL_Logo();
            Con.Log("This tool will allow you to easily downgrade your Beat Saber.");
            Con.Log("Brought to you by the", "Beat Saber Legacy Group", ConsoleColor.DarkCyan);
            Con.Space();

            //JSONSetup.Load();

            if (IsDebug) {
                Con.Log($"Environment Version is v{Environment.Version}");
                Con.Log($"OS is {Environment.OSVersion}");
                Con.Log($"Debug: IsWindows = {BuildInfo.IsWindows}");
            }

            else UpdateCheck.CheckForUpdates();

            var showError = false;
            if (BuildInfo.IsWindows) {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources") || !Directory.Exists(Environment.CurrentDirectory + "\\Depotdownloader"))
                    showError = true;
            } else {
                if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Resources") || !Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}Depotdownloader"))
                    showError = true;
            }

            if (!IsDebug) {
                var http = new HttpClient();
                http.DefaultRequestHeaders.Add("User-Agent", BuildInfo.Name);
                var contents = http.GetStringAsync("https://bslegacy.com/assets/motd.txt").GetAwaiter().GetResult();
                Con.Space();
                Con.WriteLineCentered(contents.Replace("<br>", "\n"));
                Con.Space();
                http.Dispose();
            }

            if (showError) {
                Con.Error("Please be sure you have extracted the files before running this!");
                Con.Error("Program will not run until you have extracted all the contents out of the ZIP file.");
                Con.Exit();
            } else BeginInputOption();
        }

        public static void BeginInputOption() {
            Con.WriteSeperator();

            var sys = Environment.Version; // Gets Current Installed version
            var tar = BuildInfo.TargetDotNetVer;

            if (!(sys.Major == tar.Major && sys.Minor == tar.Minor && sys.Build >= tar.Build)) {
                if (BuildInfo.IsWindows) {
                    MessageBox.Show("Make sure you have the required packages installed on your machine\n" +
                                    ".NET Desktop Runtime v6.0.0+: https://link.bslegacy.com/dotNET_6-0-8 \n" +
                                    "This MUST be installed in order to use this app properly.\n\n" +
                                    "If you already have just installed these, Press \"OK\" and ignore this message.",
                        "Required Libraries Needed", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                else {
                    try {
                        var s = File.ReadAllLines($"{AppDomain.CurrentDomain.BaseDirectory}_INSTALL THIS FIRST.txt");
                        foreach (var s1 in s) {
                            Console.WriteLine(s1);
                        }
                    }
                    catch {
                        Con.Error("_INSTALL THIS FIRST.txt is null");
                        Con.Space();
                        Con.Log("Follow the instructions on the opened webpage: https://bslegacy.com/dl/CA-Utility/LinuxInstallGuide");
                        Process.Start("https://bslegacy.com/dl/CA-Utility/LinuxInstallGuide");
                    }
                }
            }
            

            Con.Log("Select a step to get started");
            Con.InputOption("1", "\tDownload a version of Beat Saber");
            Con.InputOption("2", "\tInstall to default Steam directory", ConsoleColor.Red, "Option Removed", ConsoleColor.DarkRed);
            Con.InputOption("3", "\tMod current install");
            Con.InputOption("4", "\tConvert Songs (Change Newer songs to older format, for older games)");
            Con.InputOption("5", "\tPlay Game");
            Con.Space();
            Con.InputOption("6", "\tExit Program");
            Con.Space();
            Con.Input();
            _stepInput = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            switch (_stepInput) {
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
                    Convert.ConvertSongs();
                    break;
                case "5":
                    PlayGame.Play();
                    break;
                case "6":
                case "c":
                    Process.GetCurrentProcess().Kill();
                    break;
                case "path":
                case "ask":
                    Install.AskForPath(); // Added for debug and/or advanced usage
                    break;
                case "7":
                    if (IsDebug) InputSteamLogin();
                    else goto default;
                    break;
                default:
                    Con.Error("Invalid input, please select 1 - 6");
                    BeginInputOption();
                    break;
            }
        }

        public static void SelectGameVersion(bool dlGame) {
            Con.Log("Select which version you'd like to use", "- Type \'cancel\' to go back");

            Dictionary<ushort, StringBuilder> sb = new();
            string lastMajor = "Elly is cute!~", lastMinor = "I love you Elly~! <3"; // Can you tell I really love her?

            foreach (var v in Download.Info.Versions) {
                var s = $"  {v.Version}";
                var tVer = v.Version.Split('.');

                if (tVer.Length != 3)
                    continue;

                if (!sb.ContainsKey(v.year)) {
                    lastMajor = tVer[0];
                    lastMinor = tVer[1];
                    sb.Add(v.year, new StringBuilder(">\n\t"));
                }

                if (tVer[0] != lastMajor || tVer[1] != lastMinor) {
                    lastMajor = tVer[0];
                    lastMinor = tVer[1];
                    sb[v.year].Append($"\n\t{s}"); // AppendLine was borked
                } else
                    sb[v.year].Append(s);
            }

            Con.Space();
            foreach (var stringBuilder in sb)
                Console.WriteLine(stringBuilder.ToString().Replace("[", "").Replace(",", "").Replace("]", ""));
            Con.Space();

            Con.Input();
            _versionInput = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (_versionInput.ToLower().Contains('c'))
                BeginInputOption();

            if (dlGame)
                Download.DlGameAsync(_versionInput);
            else
                Mod.ModGame(_versionInput);
        }

        public static void InputSteamLogin() {
            Con.Log("Steam Username", "(not display name)");
            Con.SteamUN();
            _steamUsername = Console.ReadLine();
            Con.Log("Steam Password");//, "(press enter once before you start typing)");
            Con.SteamPW();
            _steamPassword = Console.ReadLine();

            if (!IsDebug) return;
            Con.Log("password is: ", _steamPassword);
            BeginInputOption();
        }
    }
}
