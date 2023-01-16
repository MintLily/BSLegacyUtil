namespace BSLegacyUtil.Utils; 

public static class StringUtils {
    public static bool ContainsMultiple(this string str1, params string[] strs) => strs.Any(str1.Contains);
    public static bool EqualsMultiple(this string str1, params string[] strs) => strs.Any(str1.Equals);
    
    public static string ReplaceAll(this string theStringToBeEdited, string oldCharacters, string newCharacters) // Idea from Java String.ReplaceAll()
        => oldCharacters.ToCharArray().Aggregate(theStringToBeEdited, (current, c) => current.Replace($"{c}", newCharacters));

}