using Pastel;

namespace BSLegacyUtil; 

public static class Console {
    public static void Log(object msg) => System.Console.WriteLine(msg);
    public static void Error(object msg) => System.Console.WriteLine(msg.ToString().Pastel("#ff0000"));
    public static void Success(object msg) => System.Console.WriteLine(msg.ToString().Pastel("#00ff00"));
    public static void Warning(object msg) => System.Console.WriteLine(msg.ToString().Pastel("#ffff00"));
    public static void CenterLog(object? msg) => System.Console.WriteLine(msg?.ToString()?.PadLeft(System.Console.WindowWidth / 2 + msg.ToString()!.Length / 2));
    public static void Space() => System.Console.WriteLine();
    
    private static void WriteLinesCentered(IList<string> lines) {
        var longestLine = lines.Max(a => a.Length);
        foreach (var line in lines)
            WriteLineCentered(line, longestLine);
    }
    
    public static void WriteLineCentered(string line, int referenceLength = -1) {
        if (referenceLength < 0)
            referenceLength = line.Length;
        System.Console.WriteLine(line.PadLeft(line.Length + System.Console.WindowWidth / 2 - referenceLength / 2));
    }
    
    public static void BsLegacyAscii() {
        var title = new List<string> {
            "    ____ _____ __                                __  ____  _ __".Pastel("#3498DB"),
            "   / __ ) ___// /   ___  ____ _____ ________  __/ / / / /_(_) /".Pastel("#557EB6"),
            "  / __  \\__ \\/ /   / _ \\/ __ `/ __ `/ ___/ / / / / / / __/ / / ".Pastel("#776592"),
            " / /_/ /__/ / /___/  __/ /_/ / /_/ / /__/ /_/ / /_/ / /_/ / /  ".Pastel("#994C6D"),
            "/_____/____/_____/\\___/\\__, /\\__,_/\\___/\\__, /\\____/\\__/_/_/   ".Pastel("#BB3249"),
            "                      /____/           /____/      ".Pastel("#DD1924") + $" v{Vars.Version}"};
        WriteHeader(title, new List<string> { "Created by " + Vars.Author });
    }
    
    public static void WriteSeparator(ConsoleColor color = ConsoleColor.White) {
        var foreColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = color;
        System.Console.WriteLine("".PadLeft(System.Console.WindowWidth, '='));
        System.Console.ForegroundColor = foreColor;
    }
    
    private static void WriteHeader(IList<string> logo, IList<string> credits) {
        //var foreColor = System.Console.ForegroundColor;
        System.Console.Title = Vars.Name + " (Console App) " + " v" + Vars.Version + " - Built by " + Vars.Author;
        //System.Console.ForegroundColor = logoColor;
        WriteLinesCentered(logo);
        System.Console.WriteLine();
        //System.Console.ForegroundColor = ConsoleColor.White;
        WriteLinesCentered(credits);
        //System.Console.ForegroundColor = foreColor;
        WriteSeparator();
    }
}