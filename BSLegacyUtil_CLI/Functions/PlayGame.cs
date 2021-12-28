using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using static BSLegacyUtil.Program;
using Newtonsoft.Json;

namespace BSLegacyUtil.Functions
{
    /*public class Values
    {
        public bool RemeberOptions { get; set; } = false;
        public bool oculus { get; set; } = false;
        public bool verbose { get; set; } = false;
        public bool fpfc { get; set; } = false;
    }

    public class JSONSetup
    {
        static readonly string path = BuildInfo.isWindows ? Environment.CurrentDirectory + "\\PlayVals.json" : $"{AppDomain.CurrentDomain.BaseDirectory}PlayVals.json";

        private static Values conf { get; set; }

        public static void Save() => File.WriteAllText(path, JsonConvert.SerializeObject(conf, Formatting.Indented));

        public static void Load()
        {
            if (!File.Exists(path)) File.WriteAllText(path, JsonConvert.SerializeObject(new Values(), Formatting.Indented));
            conf = JsonConvert.DeserializeObject<Values>(File.ReadAllText(path));
        }

        public static Values get() => conf;
        
    }*/

    //Came from https://github.com/RiskiVR/BSLegacyLauncher/blob/master/Assets/Scripts/LaunchBS.cs
    class PlayGame
    {
        //static Values get = JSONSetup.get();

        public static void Play()
        {
            string temp = BuildInfo.isWindows ? Environment.CurrentDirectory + "\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber";
            if (!Directory.Exists(temp)) {
                Con.Error("Folder or Game does not exist in current Directory, cannot play Beat Saber.");
                Con.Error("Please Install a version of Beat Saber.");
                BeginInputOption();
            }

            /*
            string sRem, soculus, sverbose, sfpfc;

            if (!get.RemeberOptions) {
                #region Remeber Options
                Con.Log("Let the program remember your options?");
                Con.Input();
                sRem = Console.ReadLine();
                Con.ResetColors();
                Con.Space();

                if (sRem == "y" || sRem == "Y" || sRem == "yes" || sRem == "Yes" || sRem == "YES") get.RemeberOptions = true;
                #endregion

                #region Ouclus Mode
                Con.Log("Run in Oculus mode?");
                Con.Input();
                soculus = Console.ReadLine();
                Con.ResetColors();
                Con.Space();

                if (soculus == "y" || soculus == "Y" || soculus == "yes" || soculus == "Yes" || soculus == "YES") get.oculus = true;
                #endregion

                #region Verbose Mode
                Con.Log("Run in Verbose mode? (BSIPA Debug mode)");
                Con.Input();
                sverbose = Console.ReadLine();
                Con.ResetColors();
                Con.Space();

                if (sverbose == "y" || sverbose == "Y" || sverbose == "yes" || sverbose == "Yes" || sverbose == "YES") get.verbose = true;
                #endregion

                #region Ouclus Mode
                Con.Log("Run in FPFC mode? (Desktop Control (noVR))");
                Con.Input();
                sfpfc = Console.ReadLine();
                Con.ResetColors();
                Con.Space();

                if (sfpfc == "y" || sfpfc == "Y" || sfpfc == "yes" || sfpfc == "Yes" || sfpfc == "YES") get.fpfc = true;
                #endregion

                JSONSetup.Save();
            }*/

            string soculus;
            bool OculusMode = false;
            Con.Log("Run in Oculus mode?");
            Con.Input();
            soculus = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (soculus.ToLower() == "y" || soculus.ToLower() == "yes") OculusMode = true;

            Process p = new Process();
            if (BuildInfo.isWindows) {
                p.StartInfo = new ProcessStartInfo($"{Environment.CurrentDirectory}\\Beat Saber\\Beat Saber.exe",
                    (OculusMode ? "-vrmode oculus " : "")/* + (get.verbose ? "--verbose" : "") + (get.fpfc ? "fpfc" : "")*/) {
                    UseShellExecute = false,
                    WorkingDirectory = $"{Environment.CurrentDirectory}\\Beat Saber",
                };
            } else {
                p.StartInfo = new ProcessStartInfo($"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber\\Beat Saber.exe",
                    (OculusMode ? "-vrmode oculus " : "")/* + (get.verbose ? "--verbose" : "") + (get.fpfc ? "fpfc" : "")*/) {
                    UseShellExecute = false,
                    WorkingDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber",
                };
            }

            try {
                p.StartInfo.Environment["SteamAppId"] = "620980";
                p.Start();
            }
            catch (Exception e) { Con.ErrorException(e.StackTrace.ToString(), e.ToString()); }
        }
    }
}
