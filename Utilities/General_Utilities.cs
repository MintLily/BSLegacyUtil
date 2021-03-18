using System;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

namespace BSLegacyUtil.Utilities
{
    class Utilities
    {
        public static void Kill()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, overwrite);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs, overwrite);
                }
            }
        }

        public static void SecurePassword()
        {
            SecureString securePwd = new SecureString();
            ConsoleKeyInfo key;
            int top, left;
            top = Console.CursorTop;
            left = Console.CursorLeft;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (securePwd.Length > 0)
                    {
                        Console.SetCursorPosition(left + securePwd.Length - 1, top);
                        Console.Write(' ');
                        Console.SetCursorPosition(left + securePwd.Length - 1, top);
                        securePwd.RemoveAt(securePwd.Length - 1);
                    }
                }
                else
                {
                    if ((securePwd.Length < 99) && (Char.IsLetterOrDigit(key.KeyChar) || key.KeyChar == '_'))
                    {
                        securePwd.AppendChar(key.KeyChar);
                        Console.SetCursorPosition(left + securePwd.Length - 1, top);
                        Console.Write('*');
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter & securePwd.Length < 99);
            securePwd.MakeReadOnly();
            securePwd.Dispose();
            Console.WriteLine();
        }

        /// <summary>
        /// Gets the console secure password.
        /// </summary>
        /// <returns></returns>
        public static string GetPassword()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return input.ToString();
        }
    }
}
