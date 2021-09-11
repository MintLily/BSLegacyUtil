using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions
{
    class Download
    {
        static string manifestID = "";
        static string gameVersion = string.Empty;
        static Process download;

        public static void DLGame(string gameVersionInput)
        {
            gameVersion = gameVersionInput;
            bool faulted = false;
            if (!Directory.Exists(Environment.CurrentDirectory + "/Beat Saber"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "/Beat Saber");

            try {
                switch (gameVersion) {
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
                    case "1.14.0":
                        manifestID = "9218225910501819399";
                        faulted = false;
                        break;
                    case "1.15.0":
                        manifestID = "7624554893344753887";
                        faulted = false;
                        break;
                    case "1.16.0":
                        manifestID = "3667184295685865706";
                        faulted = false;
                        break;
                    case "1.16.1":
                        manifestID = "9201874499606445062";
                        faulted = false;
                        break;
                    case "1.16.2":
                        manifestID = "3692829915208062825";
                        faulted = false;
                        break;
                    case "1.16.3":
                        manifestID = "6392596175313869009";
                        faulted = false;
                        break;
                    case "1.16.4":
                        manifestID = "8820433629543698585";
                        faulted = false;
                        break;
                    case "1.17.0":
                        manifestID = "7826684224434229804";
                        faulted = false;
                        break;
                    case "1.17.1":
                        manifestID = "4668547658954826996";
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
                //Con.Error(@switch.ToString());
                Con.ErrorException(@switch.StackTrace.ToString(), @switch.ToString());
            }

            if (!faulted && !string.IsNullOrWhiteSpace(manifestID)) {
                inputSteamLogin();
                try {
                    Con.Space();
                    download = Process.Start("dotnet", "Depotdownloader\\DepotDownloader.dll -app 620980 -depot 620981 -manifest " + manifestID +
                        " -username " + steamUsername + " -password " + steamPassword + " -dir \"Beat Saber\" -validate");

                    if (download != null)
                    {
                        download.WaitForExit();
                        Con.Space();
                        if (string.IsNullOrWhiteSpace(gameVersion))
                            Con.LogSuccess("Finished downloading Beat Saber");
                        else
                            Con.LogSuccess("Finished downloading Beat Saber " + gameVersion);
                        Con.Space();
                        Con.Log("Would you like to continue? [Y/N]");
                        Con.Input();
                        string @continue = Console.ReadLine();

                        if (@continue == "Y" || @continue == "y" || @continue == "YES" || @continue == "yes" || @continue == "Yes")
                            BeginInputOption();
                        else
                            Utilities.Utilities.Kill();
                    }
                }
                catch (Exception downgrade)
                {
                    //Con.Error(downgrade.ToString());
                    Con.ErrorException(downgrade.StackTrace.ToString(), downgrade.ToString());
                }
            }
        }
    }
}
