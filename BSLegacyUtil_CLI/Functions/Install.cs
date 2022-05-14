using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BSLegacyUtil.Utilities;
using Microsoft.VisualBasic.FileIO;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions
{
    public class Install
    {
        public static void InstallGame()
        {
            Con.Space();
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Con.Log("This Feature has been removed from the program");
            Con.Log("This was removed due to steam always auto-updating the game and causing frustrations within the community");
            Con.Log("Once you download the game, you can then press", "option 5", ConsoleColor.Cyan,
                $"and the run the game from within the {BuildInfo.Name} Folder", foregroundColor);
            Con.Space();
            BeginInputOption();
        }
        
        /*public static void InstallGame()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Beat Saber")) {
                Con.Error("Folder does not exist, cannot move nothing.");
                BeginInputOption();
            } else {
                AskForPath();

                try
                {
                    FileSystem.CopyDirectory("Beat Saber", "Beat Saber - Copy");
                    FileSystem.MoveDirectory("Beat Saber", _gamePath, true);
                    Con.Space();
                    Con.LogSuccess("Finished moving Files");
                    Con.Space();
                    FileSystem.RenameDirectory("Beat Saber - Copy", "Beat Saber");
                    Con.Log("If you need any help, join the Beat Saber Legacy Group discord.");
                    Con.Log("Find more information on our website:", "https://bslegacy.com", ConsoleColor.Green);
                    Con.Space();
                    Con.Log("Install plugins here:", "https://bslegacy.com/plugins", ConsoleColor.Green);
                    Con.Space();
                    Con.Log("\t\t - RiskiVR (Risk#3904)");
                    Con.Space();
                    BeginInputOption();
                }
                catch
                {
                    Con.Space();
                    Con.Error("Move Directory Failed or operation was canceled");
                    Con.Space();
                    BeginInputOption();
                }
            }
        }*/

        public static void AskForPath()
        {
            Con.Space();
            Con.Log("Current game path is ", NotFoundHandler(), ConsoleColor.Yellow);
            Con.Log("Would you like to change this?", " [Y/N]", ConsoleColor.Yellow);
            Con.Input();
            string changeLocalation = Console.ReadLine();

            if (changeLocalation == "Y" || changeLocalation == "y" || changeLocalation == "YES" || changeLocalation == "yes" || changeLocalation == "Yes")
            {
                try
                {
                    FolderSelect.InitialFolder = NotFoundHandler();
                    var dialogResult = FolderSelect.ShowDialog();
                    _gamePath = FolderSelect.Folder;
                }
                catch { Con.Error("Select Folder Failed"); BeginInputOption(); }
            }
            else _gamePath = NotFoundHandler();
        }

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
                            PathLogic.installPath = pathedited;
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
