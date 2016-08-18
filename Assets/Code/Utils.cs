using UnityEngine;
using System.Collections;
using System.Linq;

public class Utils {

    public Utils()
    {

    }

    public static Color HexToColor(string hex, byte alpha = 255)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, alpha);
    }

    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        const string charNums = "abcdefghijklmnopqrstuvwxyz0123456789";

        string alphas = new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Range(0, chars.Length)]).ToArray());
        string nums = new string(Enumerable.Repeat(charNums, length-5).Select(s => s[Random.Range(0, charNums.Length)]).ToArray());

        return alphas + nums;
    }
}
