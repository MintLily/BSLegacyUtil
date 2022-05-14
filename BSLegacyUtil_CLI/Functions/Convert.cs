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
    public class Convert
    {
        public static void ConvertSongs()
        {
            string temp = BuildInfo.IsWindows ? $"{Environment.CurrentDirectory}\\Beat Saber" : $"{AppDomain.CurrentDomain.BaseDirectory}Beat Saber";
            if (_gamePath != temp)
                Install.AskForPath();
            if (!Directory.Exists(_gamePath + "\\CustomSongs"))
                Directory.CreateDirectory(_gamePath + "\\CustomSongs");

            try { FileSystem.CopyDirectory(_gamePath + "\\Beat Saber_Data\\CustomLevels", _gamePath + "\\CustomSongs", true); }
            catch (Exception e) { Con.Error(e.ToString()); }

            string[] array = Directory.GetDirectories(_gamePath + "\\CustomSongs");

            Process yeet = null;
            foreach (var Directories in array)
                if (!Directories.Contains("Flygplan") && !Directories.Contains("Middle Milk - Beards"))
                    yeet = Process.Start(_gamePath + "\\CustomSongs\\songe-unconverter.exe", $"\"{Directories}\"");

            if (yeet != null)
                yeet.WaitForExit();

            Con.LogSuccess("Finished Converting songs");
            Con.Continue();
            BeginInputOption();
        }
    }
}
