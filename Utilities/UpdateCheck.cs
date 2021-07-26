using System.Net;
using System.Text;
using static BSLegacyUtil.BuildInfo;

namespace BSLegacyUtil.Utilities
{
    class UpdateCheck
    {
        private static string lazyTag = "\"tag_name\": \"v" + Version + "\"";
        private static string incomingData = string.Empty;
        private static string GitHub = "https://api.github.com/repos/" + Author + "/BSLegacyUtil/releases";

        public static void CheckForUpdates()
        {
            try {
                WebClient web = new WebClient();
                web.Headers["Content-Type"] = "application/json"; // Looks for JSON format
                web.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0"); // Adds a "fake" client request
                web.Encoding = Encoding.UTF8; // Get as plain text (No Special characters)
                incomingData = web.DownloadString(GitHub);

                if (incomingData.Contains(lazyTag)) {
                    if (Program.isDebug) Con.Log("Versions are matching");
                } else Con.SendUpdateNotice();
            }
            catch { Con.Error("Could not check for update. Please make sure you are connected to your network."); }
        }
    }
}
