namespace BSLegacyUtil.Utils; 

public static class StringUtils {
    public static bool ContainsMultiple(this string str1, string str2, string str3) => str1.Contains(str2) || str1.Contains(str3);
    public static bool ContainsMultiple(this string str1, string str2, string str3, string str4) => str1.Contains(str2) || str1.Contains(str3) || str1.Contains(str4);
    public static bool ContainsMultiple(this string str1, string str2, string str3, string str4, string str5) => str1.Contains(str2) || str1.Contains(str3) || str1.Contains(str4) || str1.Contains(str5);
    
    public static bool EqualsMultiple(this string str1, string str2, string str3) => str1.Equals(str2) || str1.Equals(str3);
    public static bool EqualsMultiple(this string str1, string str2, string str3, string str4) => str1.Equals(str2) || str1.Equals(str3) || str1.Equals(str4);
    public static bool EqualsMultiple(this string str1, string str2, string str3, string str4, string str5) => str1.Equals(str2) || str1.Equals(str3) || str1.Equals(str4) || str1.Equals(str5);
    
    public static string ReplaceAll(this string theStringToBeEdited, string oldCharacters, string newCharacters) // Idea from Java String.ReplaceAll()
        => oldCharacters.ToCharArray().Aggregate(theStringToBeEdited, (current, c) => current.Replace($"{c}", newCharacters));

}