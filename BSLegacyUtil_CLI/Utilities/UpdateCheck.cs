using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static BSLegacyUtil.BuildInfo;

namespace BSLegacyUtil.Utilities {
    public class UpdateCheck {
        private static readonly string LazyTag = "\"tag_name\": \"" + Version + "\"";
        private static readonly string GitHub = "https://api.github.com/repos/" + Author + "/BSLegacyUtil/releases";

        private static async Task<string> GetString() {
            var web = new HttpClient();
            web.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0"); // Adds a "fake" client request
            //web.DefaultRequestHeaders.Add("Content-Type", "application/json"); // Looks for JSON format
            return await web.GetStringAsync(GitHub);
        }

        public static void CheckForUpdates() {
            try {
                var s = GetString().GetAwaiter().GetResult();

                if (s.Contains(LazyTag)) {
                    if (Program.IsDebug) Con.Log("Versions are matching");
                } else Con.SendUpdateNotice();
            }
            catch { Con.Error("Could not check for update. Please make sure you are connected to your network."); }
        }
    }
}
