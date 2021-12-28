using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using Microsoft.VisualBasic.FileIO;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions
{
    class Install
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
                    FileSystem.MoveDirectory("Beat Saber", gamePath, true);
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
            Con.Log("Current game path is ", PathLogic.NotFoundHandler(), ConsoleColor.Yellow);
            Con.Log("Would you like to change this?", " [Y/N]", ConsoleColor.Yellow);
            Con.Input();
            string changeLocalation = Console.ReadLine();

            if (changeLocalation == "Y" || changeLocalation == "y" || changeLocalation == "YES" || changeLocalation == "yes" || changeLocalation == "Yes")
            {
                try
                {
                    FolderSelect.InitialFolder = PathLogic.NotFoundHandler();
                    FolderSelect.ShowDialog();
                    gamePath = FolderSelect.Folder;
                }
                catch { Con.Error("Select Folder Failed"); BeginInputOption(); }
            }
            else gamePath = PathLogic.NotFoundHandler();
        }
    }
}
