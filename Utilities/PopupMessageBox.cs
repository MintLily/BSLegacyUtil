using ModernMessageBoxLib;
using System.Windows.Media;

namespace BSLegacyUtil.Utilities {
    public class PopupMessageBox {
        public static void ShowBox() {
            QModernMessageBox.MainLang = new() {
                Ok = "Ok",
                Cancel = "Cancel",
                Abort = "Abort",
                Ignore = "Ignore",
                No = "No",
                Yes = "Yes",
                Retry = "Retry"
            };
            QModernMessageBox.GlobalBackground = new SolidColorBrush(Colors.Yellow) { Opacity = 0.6 };
            QModernMessageBox.GlobalForeground = Brushes.Black;

            QModernMessageBox.Show("Make sure you have the required packages installed on your machine\n" +
                                   ".NET Runtime v3.1.16+: https://link.bslegacy.com/dotNET_3-1-16 \n" +
                                   ".NET Runtime v5.0.10+: https://link.bslegacy.com/dotNET_5-0-10 \n\n" +
                                   "These MUST be installed in order to use this app properly.",
                "Required Libraries Needed", QModernMessageBox.QModernMessageBoxButtons.Ok, ModernMessageboxIcons.Warning);
        }

        /*
        public static IndeterminateProgressWindow win;

        public static async void ProgressWindow(string gameVer) {
            IndeterminateProgressWindow.GlobalBackground = new SolidColorBrush(Colors.DarkBlue) { Opacity = 0.6 };
            IndeterminateProgressWindow.GlobalForeground = Brushes.WhiteSmoke;
            await Task.Delay(2000);
            win = new IndeterminateProgressWindow($"Downloading Beat Saber v{gameVer}");
            win.Show();
        }

        public static async void ProgWinTextWithDelay(string text, int delay = 5000) {
            win.Message = text;
            await Task.Delay(delay);
            win.Close();
        }
        */
    }
}