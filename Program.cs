using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BSLegacyUtil.Utilities;

namespace BSLegacyUtil
{
    public class BuildInfo
    {
        public const string Name = "BSLegacyUtil";
        public const string Version = "0.0.3";
        public const string Author = "Korty";
    }

    class Program
    {
        public static bool isDebug;

        static string steamUsername;
        static string steamPassword;
        static string stepInput;
        static string versionInput;

        static string gamePath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber";

        static void Main(string[] args)
        {
            Console.Title = BuildInfo.Name + " v" + BuildInfo.Version + " - Built by " + BuildInfo.Author;

            if (Environment.CommandLine.Contains("debug"))
            {
                isDebug = true;
                Con.Init();
            }
            Con._Logo();
            Con.Space();
            Con.Log("Welcome to our easy installation of Archived Beat Saber versions!");
            Con.Log("Brought to you by the Beat Saber Legacy Group.");
            Con.Space();
            Con.Log("Please be sure you have extracted the files before running this!");
            Con.Space();

            BeginInputOption();
        }

        #region Main Functions
        static void BeginInputOption()
        {
            Con.Log("Select a step to get started");
            Begin:
            Con.InputOption("1", "\tDownload a version of Beat Saber");
            Con.InputOption("2", "\tInstall to default Steam directory");
            Con.InputOption("3", "\tMod current install");
            Con.InputOption("4", "\tConvert Songs");
            Con.Space();
            Con.InputOption("5", "\tExit Program");
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
                    InstallGame();
                    break;
                case "3":
                    SelectGameVersion(false);
                    break;
                case "4":
                    convertSongs();
                    break;
                case "5":
                    Process.GetCurrentProcess().Kill();
                    break;
                default:
                    Con.Error("Invalid input, please select 1 - 4");
                    goto Begin;
            }
        }

        static void SelectGameVersion(bool dlGame)
        {
            Con.Log("Select which version you'd like to use");
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
            Con.Input();
            versionInput = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (dlGame)
                DLGame(versionInput);
            else
                modGame(versionInput);
        }

        static void InstallGame()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "/Beat Saber"))
                Con.Error("Folder does not exist, cannot move nothing.");
            else
            {
                Utilities.Utilities.DirectoryCopy("Beat Saber", gamePath, true, true);
                Con.Log("If you need any help, join the Beat Saber Legacy Group discord.");
                Con.Log("Find more information on our website: https://bslegacy.com");
                Con.Space();
                Con.Log("Install plugins here: https://bslegacy.com/plugins");
                Con.Space();
                Con.Log("\t\t - RiskiVR (Risk#3904)");
            }
        }

        static void modGame(string gameVersion)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "/Resources"))
                Con.Error("Folder does not exist, cannot move nothing.");
            else
            {
                switch (gameVersion)
                {
                    case "0.10.0":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.10.0", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    case "0.10.1":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.10.0", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    case "0.10.2":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.10.0", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    case "0.10.2p2":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.10.0", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    case "0.11.1":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.11.1", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    case "0.11.2":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.11.2", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    case "0.12.2":
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber 0.12.2", gamePath, true, true);
                        Utilities.Utilities.DirectoryCopy("Resources/CustomSongs", gamePath + "/CustomSongs", true, true);
                        Process.Start(gamePath + "IPA.exe", gamePath + "Beat Saber.exe");
                        Con.Log("Finished modding game");
                        break;
                    default:
                        Utilities.Utilities.DirectoryCopy("Resources/Beat Saber_Data", gamePath + "/Beat Saber_Data", true, true);
                        Process.Start("Resources/ModAssistant.exe");
                        Con.Log("If you need any help, join the Beat Saber Legacy Group discord.");
                        Con.Log("Find more information on our website: https://bslegacy.com");
                        Con.Space();
                        Con.Log("Install extra plugins here: https://bslegacy.com/plugins");
                        Con.Space();
                        Con.Log("\t\t - RiskiVR (Risk#3904)");
                        break;
                }
            }
        }

        static void convertSongs()
        {
            Con.Error("Feature not yet implemented");
            Con.Space();
            BeginInputOption();
        }

        static void inputSteamLogin()
        {
            Con.Log("Steam Username (not display name)");
            Con.SteamUN();
            steamUsername = Console.ReadLine();
            Con.Log("Steam Password (press enter twice)");
            Con.SteamPW();
            steamPassword = Console.ReadLine();
        }
        #endregion

        #region DL Game Versions
        static string manifestID = "";

        static void DLGame(string gameVersion)
        {
            bool faulted = false;
            if (!Directory.Exists(Environment.CurrentDirectory + "/Beat Saber"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Beat Saber");
            
            try
            {
                switch (gameVersion)
                {
                    case "0.10.1":
                        manifestID = "6316038906315325420";
                        //faulted = false;
                        break;
                    case "0.10.2":
                        manifestID = "2542095265882143144";
                        faulted = false;
                        break;
                    case "0.10.2p1":
                        manifestID = "5611588554149133260";
                        faulted = false;
                        break;
                    case "0.11.0":
                        manifestID = "8700049030626148111";
                        faulted = false;
                        break;
                    case "0.11.1":
                        manifestID = "6574193224879562324";
                        faulted = false;
                        break;
                    case "0.11.2":
                        manifestID = "2707973953401625222";
                        faulted = false;
                        break;
                    case "0.12.0":
                        manifestID = "6094599000655593822";
                        faulted = false;
                        break;
                    case "0.12.0p1":
                        manifestID = "2068421223689664394";
                        faulted = false;
                        break;
                    case "0.12.1":
                        manifestID = "2472041066434647526";
                        faulted = false;
                        break;
                    case "0.12.2":
                        manifestID = "5325635033564462932";
                        faulted = false;
                        break;
                    case "0.13.0":
                        manifestID = "3102409495238838111";
                        faulted = false;
                        break;
                    case "0.13.0p1":
                        manifestID = "6827433614670733798";
                        faulted = false;
                        break;
                    case "0.13.1":
                        manifestID = "6033025349617217666";
                        faulted = false;
                        break;
                    case "0.13.2":
                        manifestID = "6839388023573913446";
                        faulted = false;
                        break;
                    case "1.0.0":
                        manifestID = "152937782137361764";
                        faulted = false;
                        break;
                    case "1.0.1":
                        manifestID = "7950322551526208347";
                        faulted = false;
                        break;
                    case "1.1.0":
                        manifestID = "1400454104881094752";
                        faulted = false;
                        break;
                    case "1.0.0p1":
                        manifestID = "1041583928494277430";
                        faulted = false;
                        break;
                    case "1.2.0":
                        manifestID = "3820905673516362176";
                        faulted = false;
                        break;
                    case "1.3.0":
                        manifestID = "2440312204809283162";
                        faulted = false;
                        break;
                    case "1.4.0":
                        manifestID = "3532596684905902618";
                        faulted = false;
                        break;
                    case "1.4.2":
                        manifestID = "1199049250928380207";
                        faulted = false;
                        break;
                    case "1.5.0":
                        manifestID = "2831333980042022356";
                        faulted = false;
                        break;
                    case "1.6.0":
                        manifestID = "1869974316274529288";
                        faulted = false;
                        break;
                    case "1.6.1":
                        manifestID = "6122319670026856947";
                        faulted = false;
                        break;
                    case "1.6.2":
                        manifestID = "4932559146183937357";
                        faulted = false;
                        break;
                    case "1.7.0":
                        manifestID = "3516084911940449222";
                        faulted = false;
                        break;
                    case "1.8.0":
                        manifestID = "3177969677109016846";
                        faulted = false;
                        break;
                    case "1.9.0":
                        manifestID = "7885463693258878294";
                        faulted = false;
                        break;
                    case "1.9.1":
                        manifestID = "6222769774084748916";
                        faulted = false;
                        break;
                    case "1.10.0":
                        manifestID = "6711131863503994755";
                        faulted = false;
                        break;
                    case "1.11.0":
                        manifestID = "1919603726987963829";
                        faulted = false;
                        break;
                    case "1.11.1":
                        manifestID = "3268824881806146387";
                        faulted = false;
                        break;
                    case "1.12.1":
                        manifestID = "2928416283534881313";
                        faulted = false;
                        break;
                    case "1.12.2":
                        manifestID = "543439039654962432";
                        faulted = false;
                        break;
                    case "1.13.0":
                        manifestID = "4635119747389290346";
                        faulted = false;
                        break;
                    case "1.13.2":
                        manifestID = "8571679771389514488";
                        faulted = false;
                        break;
                    case "1.13.4":
                        manifestID = "1257277263145069282";
                        faulted = false;
                        break;
                    case "1.13.5":
                        manifestID = "7007516983116400336";
                        faulted = false;
                        break;
                    default:
                        manifestID = "";
                        faulted = true;
                        Con.Error("Invalid input. Please input a valid version number.");
                        Con.Space();
                        SelectGameVersion(true);
                        break;
                }
            }
            catch (Exception @switch)
            {
                Con.Error(@switch.ToString());
            }

            if (!faulted)
            {
                inputSteamLogin();
                try
                {
                    Process downgrade = Process.Start("dotnet", "Depotdownloader\\DepotDownloader.dll -app 620980 -depot 620981 -manifest " + manifestID +
                        " -username " + steamUsername + " -password " + steamPassword + " -dir \"Beat Saber\" -validate");
                    try
                    {
                        downgrade.WaitForExit();
                        downgrade.BeginOutputReadLine();
                        string processOutput = downgrade.StandardOutput.ReadToEnd();
                        if (processOutput.Contains("InvalidPassword"))
                            Con.Error("InvalidPassword, try again");
                        else
                            Con.LogSuccess("Finished downloading Beat Saber " + gameVersion);
                        Con.Space();
                        BeginInputOption();
                    }
                    catch
                    {
                        Con.Space();
                        Con.Error("InvalidPassword, please try again");
                        
                        BeginInputOption();
                    }
                }
                catch (Exception downgrade)
                {
                    Con.Error(downgrade.ToString());
                }
            }
            else
                Process.GetCurrentProcess().Kill();
        }
        #endregion
    }
}
