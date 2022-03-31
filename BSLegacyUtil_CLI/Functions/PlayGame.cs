using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using static BSLegacyUtil.Program;
using Newtonsoft.Json;
using System.Linq;

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

            string soculus;
            bool OculusMode = false;
            Con.Log("Run in Oculus mode?");
            Con.Input();
            soculus = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (soculus.ToLower() == "y" || soculus.ToLower() == "yes") OculusMode = true;
            
            LaunchGame(OculusMode);
        }

        public static void LaunchGame(bool oculusMode, bool newPath = false, string[] args = null) {
            var p = new Process();

            var cl = Environment.CommandLine;
            string temp = "";
            bool temp_hasPath = false, final;

            if (cl.Contains("--path=")) {
                temp = args.Any(x => x.StartsWith("--path=")).ToString().Replace("--path=", "");
                temp_hasPath = true;
            }

            if (temp_hasPath) {
                if (Directory.Exists(temp.Replace("Beat Saber.exe", "")))
                    final = Directory.Exists(temp.Replace("Beat Saber.exe", ""));
                else
                    final = Directory.Exists(BuildInfo.isWindows ? $"{Environment.CurrentDirectory}\\" : $"{AppDomain.CurrentDomain.BaseDirectory}");
            } else
                final = Directory.Exists(BuildInfo.isWindows ? $"{Environment.CurrentDirectory}\\" : $"{AppDomain.CurrentDomain.BaseDirectory}");

            if (string.IsNullOrEmpty(temp))
                temp = BuildInfo.isWindows ? $"{Environment.CurrentDirectory}\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber";

            p.StartInfo = new ProcessStartInfo($"{temp}Beat Saber.exe", oculusMode ? "-vrmode oculus " : "") {
                UseShellExecute = false,
                WorkingDirectory = temp,
            };

            try {
                p.StartInfo.Environment["SteamAppId"] = "620980";
                p.Start();
            }
            catch (Exception e) {
                Con.ErrorException(e.StackTrace, e.ToString());
            }
        }
    }
}
