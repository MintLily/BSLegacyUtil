using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions
{
    class PlayGame
    {
        static bool oculus, verbose, fpfc;
        public static void Play()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Beat Saber")) {
                Con.Error("Folder or Game does not exist in current Directory, cannot play Beat Saber.");
                Con.Error("Please Install a version of Beat Saber.");
                BeginInputOption();
            }

            string soculus, sverbose, sfpfc;

            #region Ouclus Mode
            Con.Log("Run in Oculus mode?");
            Con.Input();
            soculus = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (soculus == "y" || soculus == "Y" || soculus == "yes" || soculus == "Yes" || soculus == "YES") oculus = true;
            #endregion

            #region Verbose Mode
            Con.Log("Run in Verbose mode? (BSIPA Debug mode)");
            Con.Input();
            sverbose = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (sverbose == "y" || sverbose == "Y" || sverbose == "yes" || sverbose == "Yes" || sverbose == "YES") verbose = true;
            #endregion

            #region Ouclus Mode
            Con.Log("Run in FPFC mode? (Desktop Control (noVR))");
            Con.Input();
            sfpfc = Console.ReadLine();
            Con.ResetColors();
            Con.Space();

            if (sfpfc == "y" || sfpfc == "Y" || sfpfc == "yes" || sfpfc == "Yes" || sfpfc == "YES") fpfc = true;
            #endregion


            Process p = new Process();
            p.StartInfo = new ProcessStartInfo($"{Environment.CurrentDirectory}\\Beat Saber\\Beat Saber.exe",
                (oculus ? "-vrmode oculus " : "") + (verbose ? "--verbose" : "") + (fpfc ? "fpfc" : "")) {
                UseShellExecute = false,
                WorkingDirectory = $"{Environment.CurrentDirectory}\\Beat Saber",
            };

            try {
                p.StartInfo.Environment["SteamAppId"] = "620980";
                p.Start();
            }
            catch (Exception e) { Con.Error(e.ToString()); }
        }
    }
}
