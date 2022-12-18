using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using BSLegacyUtil.Data;
using Pastel;


namespace BSLegacyUtil.Utils; 

public static class UpdateUtil {
    public static void PrintSha256ForDebug() {
        var _256 = GetSha256();
        Vars.InternalSha256 = _256;
        Console.Log($"SHA256: {_256}");
    }
    
    private static string GetSha256() {
        var file = File.ReadAllBytes($"{Vars.BaseDirectory}BSLegacyUtil.exe");
        var hash = SHA256.Create();
        return GetHash(hash, file);
    }
    
    private static string GetHash(HashAlgorithm sha256, byte[] data) {
        var rawBytes = sha256.ComputeHash(data);
        var stringBuilder = new StringBuilder();
        foreach (var b in rawBytes)
            stringBuilder.Append(b.ToString("x2"));

        return stringBuilder.ToString();
    }

    public static void VerifyFileIntegrity(bool justSkipAndShowFailedMessage = false) {
        Vars.FileIntegrityFailed = false;
        if (ProgramJsonModel.TheProgramData.ExpectedVersion != Vars.Version) {
            Console.CenterLog("Version mismatch, please update the program.");
            return;
        }

        if ((ProgramJsonModel.TheProgramData.ExpectedVersion == Vars.Version &&
             ProgramJsonModel.TheProgramData.ExpectedSha256 != Vars.InternalSha256) || justSkipAndShowFailedMessage) {
            Vars.FileIntegrityFailed = true;
            Console.CenterLog("File integrity check failed.".Pastel("#ff0000"));
            Console.CenterLog("This could be from a modified and potentially malicious and dangerous version of the program.".Pastel("#ABAAA9"));
            Console.Space();
            Console.WriteSeparator(ConsoleColor.Red);
        }
    }
}