using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BSLegacyUtil.Utilities
{
    // Came from https://github.com/Slaynash/VRChatModInstaller/blob/074fe127b26945c622c0afcd9941aa8a9b0632b2/VRCModManager/Core/PathLogic.cs
    public class PathLogic
    {
        public static string installPath;
        //public Platform platform;

        private const int SteamAppId = 620980;
        private const string AppFileName = "Beat Saber.exe";

        /*public static string GetInstallLocation()
        {
            string steam = GetSteamLocation();
            if (steam != null)
            {
                if (Directory.Exists(steam))
                {
                    if (File.Exists(Path.Combine(steam, AppFileName)))
                    {
                        //platform = Platform.Steam;
                        installPath = steam;
                        return steam;
                    }
                }
            }
            string fallback = GetFallbackDirectory();
            installPath = fallback;
            return fallback;
        }

        private static string GetFallbackDirectory()
        {
            MessageBox.Show("We couldn't seem to find your Beat Saber installation, please press \"OK\" and point us to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return NotFoundHandler();
        }

        public static string GetSteamLocation()
        {
            try
            {
                var steamFinder = new SteamFinder();
                if (!steamFinder.FindSteam())
                    return null;

                return steamFinder.FindGameFolder(SteamAppId);
            }
            catch (Exception ex)
            {
                return null;
            }

        }*/

        public static string NotFoundHandler() {
            bool found = false;
            while (found == false) {
                using (var folderDialog = new OpenFileDialog()) {
                    folderDialog.Title = "Select Beat Saber.exe";
                    folderDialog.FileName = "Beat Saber.exe";
                    folderDialog.Filter = "Beat Saber Executable|Beat Saber.exe";
                    if (folderDialog.ShowDialog() == DialogResult.OK) {
                        string path = folderDialog.FileName;
                        if (path.Contains("Beat Saber.exe")) {
                            string pathedited = path.Replace(@"\Beat Saber.exe", "");
                            installPath = pathedited;
                            return pathedited;
                        } else {
                            MessageBox.Show("The directory you selected doesn't contain Beat Saber.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else {
                        return null;
                    }
                }
            }
            return string.Empty;
        }
    }
}
