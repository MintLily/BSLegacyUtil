using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using BSLegacyUtil.Utilities;
using Microsoft.VisualBasic.FileIO;
using static BSLegacyUtil.Program;

namespace BSLegacyUtil.Functions
{
    class Convert
    {
        public static void convertSongs()
        {
            string temp = BuildInfo.isWindows ? $"{Environment.CurrentDirectory}\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber";
            if (gamePath != temp)
                Install.AskForPath();
            if (!Directory.Exists(gamePath + "\\CustomSongs"))
                Directory.CreateDirectory(gamePath + "\\CustomSongs");

            try { FileSystem.CopyDirectory(gamePath + "\\Beat Saber_Data\\CustomLevels", gamePath + "\\CustomSongs", true); }
            catch (Exception e) { Con.Error(e.ToString()); }

            string[] array = Directory.GetDirectories(gamePath + "\\CustomSongs");

            Process yeet = null;
            foreach (var Directories in array)
                if (!Directories.Contains("Flygplan") && !Directories.Contains("Middle Milk - Beards"))
                    yeet = Process.Start(gamePath + "\\CustomSongs\\songe-unconverter.exe", $"\"{Directories}\"");

            if (yeet != null)
                yeet.WaitForExit();

            Con.LogSuccess("Finished Converting songs");
            Con.Continue();
            BeginInputOption();
        }
    }
}
