using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static BSLegacyUtil.BuildInfo;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BSLegacyUtil
{
    public class Con
    {
		private static int errorCount = 0;
		private static StreamWriter log;
		private static readonly string fileprefix = BuildInfo.Name + "_";

		internal static void Init()
		{
			if (log == null)
			{
				string text = Environment.CurrentDirectory + "\\Logs\\" + 
					fileprefix + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff") + ".log";
				FileInfo fileInfo = new FileInfo(text);
				DirectoryInfo directoryInfo = new DirectoryInfo(fileInfo.DirectoryName);
				if (!directoryInfo.Exists) directoryInfo.Create();
				else CleanOld(directoryInfo);
				FileStream stream;
				if (!fileInfo.Exists) stream = fileInfo.Create();
				else stream = new FileStream(text, FileMode.Open, FileAccess.Write, FileShare.Read);
				log = new StreamWriter(stream);
				log.AutoFlush = true;
			}
			Log("ConsoleOutput and Error Logs Initialized");
		}

		internal static void Stop()
		{
			if (log != null) log.Close();
		}

		internal static void CleanOld(DirectoryInfo logDirInfo)
		{
			FileInfo[] files = logDirInfo.GetFiles(fileprefix + "*");
			if (files.Length != 0)
			{
				List<FileInfo> list = (from x in files.ToList<FileInfo>()
									   orderby x.LastWriteTime
									   select x).ToList<FileInfo>();
				for (int i = list.Count - 10; i > -1; i--) list[i].Delete();
			}
		}

		internal static string GetTimestamp() => DateTime.Now.ToString("HH:mm:ss.fff");

		public static void Space() => Console.WriteLine("");

		public static void Log(string s)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
            {
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine("] " + s);
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void Log(string s, string extra, ConsoleColor color = ConsoleColor.Red)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] " + s);
			Console.ForegroundColor = color;
			Console.WriteLine(" " + extra);
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void Log(string s, string s2, ConsoleColor s2color, string s3, ConsoleColor s3color)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] " + s);
			Console.ForegroundColor = s2color;
			Console.Write(" " + s2);
			Console.ForegroundColor = s3color;
			Console.WriteLine(" " + s3);
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void LogSuccess(string s)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Success");
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine("] " + s);
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

        public static void LogFail(string s) {
            ResetColors();
            ConsoleColor foregroundColor = Console.ForegroundColor;
            string timestamp = GetTimestamp();
            if (Program.IsDebug) {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(timestamp);
                Console.ForegroundColor = foregroundColor;
                Console.Write("] [");
            } else
                Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(Name);
            Console.ForegroundColor = foregroundColor;
            Console.Write("] [");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Failed");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine("] " + s);
            Console.ForegroundColor = foregroundColor;
            if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
        }

		public static void Input()
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("INPUT");
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] "); }
		}

		public static void ResetColors() => Console.ForegroundColor = ConsoleColor.White;

		public static void InputOption(string num, string s, bool useable = true)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(num);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			if (useable)
				Console.WriteLine(s);
			else
            {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(s);
			}
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void InputOption(string num, string s, ConsoleColor color, string s2, ConsoleColor s2color)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(num);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			Console.ForegroundColor = color;
			Console.Write(s);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(" --- ");
			Console.ForegroundColor = s2color;
			Console.WriteLine(s2);
			Console.ForegroundColor = foregroundColor;
			if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
		}

		public static void SteamUN()
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("UserName");
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			if (log != null) { log.Write(""); }
		}

		public static void SteamPW()
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			string timestamp = GetTimestamp();
			if (Program.IsDebug)
			{
				Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write(timestamp);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
			}
			else
				Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(Name);
			Console.ForegroundColor = foregroundColor;
			Console.Write("] [");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Password");
			Console.ForegroundColor = foregroundColor;
			Console.Write("] ");
			//Utilities.Utilities.SecurePassword();
			if (log != null) { log.Write(""); }
		}

		public static void Error(string s)
		{
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			if (errorCount < 255)
			{
				string timestamp = GetTimestamp();
				if (Program.IsDebug)
				{
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(timestamp);
					Console.ForegroundColor = foregroundColor;
					Console.Write("] [");
				}
				else
					Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(Name);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error");
				Console.ForegroundColor = foregroundColor;
				Console.Write("] ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(s);
				Console.ForegroundColor = foregroundColor;
				if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + s); }
				errorCount++;
			}
			if (errorCount == 255)
			{
				string timestamp2 = GetTimestamp();
				if (Program.IsDebug)
				{
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(timestamp2);
					Console.ForegroundColor = foregroundColor;
					Console.Write("] [");
				}
				else
					Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(Name);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error");
				Console.ForegroundColor = foregroundColor;
				Console.Write("] ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("The console error limit has been reached.");
				Console.ForegroundColor = foregroundColor;
				if (log != null) { log.WriteLine("[" + timestamp2 + "] [" + Name + "] " + s); }
				errorCount++;
			}
		}

		public static void ErrorException(string stack, string error)
        {
			ResetColors();
			ConsoleColor foregroundColor = Console.ForegroundColor;
			if (errorCount < 255)
			{
				string timestamp = GetTimestamp();
				if (Program.IsDebug)
				{
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(timestamp);
					Console.ForegroundColor = foregroundColor;
					Console.Write("] [");
				}
				else
					Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(Name);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error");
				Console.ForegroundColor = foregroundColor;
				Console.Write("] ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("====== STACK =====\n" + stack + "\n ===== ERROR =====\n" + error);
				Console.ForegroundColor = foregroundColor;
				if (log != null) { log.WriteLine("[" + timestamp + "] [" + Name + "] " + "====== STACK =====\n" + stack + "\n ===== ERROR =====\n" + error); }
				errorCount++;
			}
			if (errorCount == 255)
			{
				string timestamp2 = GetTimestamp();
				if (Program.IsDebug)
				{
					Console.Write("[");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(timestamp2);
					Console.ForegroundColor = foregroundColor;
					Console.Write("] [");
				}
				else
					Console.Write("[");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(Name);
				Console.ForegroundColor = foregroundColor;
				Console.Write("] [");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error");
				Console.ForegroundColor = foregroundColor;
				Console.Write("] ");
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("The console error limit has been reached.");
				Console.ForegroundColor = foregroundColor;
				if (log != null) { log.WriteLine("[" + timestamp2 + "] [" + Name + "] " + "====== STACK =====\n" + stack + "\n ===== ERROR =====\n" + error); }
				errorCount++;
			}
		}

		public static void WriteHeader(IList<string> logo, ConsoleColor logoColor, IList<string> credits)
		{
			var foreColor = Console.ForegroundColor;

			Console.Title = Name + " (Console App) " + " v" + BuildInfo.Version + " - Built by " + Author;

			Console.ForegroundColor = logoColor;
			WriteLinesCentered(logo);

			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.White;
			WriteLinesCentered(credits);

			Console.ForegroundColor = foreColor;

			WriteSeperator();
		}

		private static void WriteLinesCentered(IList<string> lines)
		{
			var longestLine = lines.Max(a => a.Length);
			foreach (var line in lines)
				WriteLineCentered(line, longestLine);
		}

		public static void WriteLineCentered(string line, int referenceLength = -1) {
			if (referenceLength < 0)
				referenceLength = line.Length;

			Console.WriteLine(line.PadLeft(line.Length + Console.WindowWidth / 2 - referenceLength / 2));
		}

		public static void SendUpdateNotice()
        {
			var foreColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			WriteSeperator(ConsoleColor.Red);
			WriteLineCentered("A newer version of " + Name + " is avaiable!");
			WriteSeperator(ConsoleColor.Red);
			Console.ForegroundColor = foreColor;
		}

		public static void BSL_Logo() {
			var title = new List<string>() {
                "    ____ _____ __                                __  ____  _ __",
                "   / __ ) ___// /   ___  ____ _____ ________  __/ / / / /_(_) /",
                "  / __  \\__ \\/ /   / _ \\/ __ `/ __ `/ ___/ / / / / / / __/ / / ",
                " / /_/ /__/ / /___/  __/ /_/ / /_/ / /__/ /_/ / /_/ / /_/ / /  ",
                "/_____/____/_____/\\___/\\__, /\\__,_/\\___/\\__, /\\____/\\__/_/_/   ",
                "                      /____/           /____/                  ",};
			WriteHeader(title, ConsoleColor.Cyan, new List<string> { "Created by " + Author });
		}

		public static void WriteSeperator(ConsoleColor color = ConsoleColor.White)
		{
			var foreColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine("".PadLeft(Console.WindowWidth, '='));
			Console.ForegroundColor = foreColor;
		}

		public static void Exit(int exitCode = 0)
		{
			Log("Press Enter to exit.");
			Console.ReadLine();
			Environment.Exit(exitCode);
		}

		public static void Continue()
		{
			Log("Press Enter to contiue . . .");
			Console.ReadLine();
		}
	}
}
