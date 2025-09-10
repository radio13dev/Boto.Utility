using System;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

public static class StringUtilities
{
    public static string ToHourString(int hour)
    {
        if (hour == 0)
            return "12am";
        else if (hour == 12)
            return hour.ToString() + "pm";
        else if (hour <= 11)
            return hour.ToString() + "am";
        else
            return (hour - 12).ToString() + "pm";
    }
        
    public static string ToOrdinal(int num)
    {
        if (num <= 0) return num.ToString();

        switch (num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch (num % 10)
        {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }
    }

    public static string GetPluralString(float f, string noPlural, string plural)
    {
        if (math.abs(f) == 1)
        {
            return noPlural;
        }
        return plural;
    }

    public static string GetPluralString(int i, string noPlural, string plural)
    {
        //Stop overflow exception
        if (i == int.MinValue)
        {
            i = 0;
        }
        if (math.abs(i) == 1)
        {
            return noPlural;
        }
        return plural;
    }

    public static string ToRoman(int number)
    {
        if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);
        throw new ArgumentOutOfRangeException("something bad happened");
    }

    public static string FormatPercentage(float ZeroOneMultiplier)
    {
        var bigNumber = ZeroOneMultiplier *= 100;
        var bigNumberInt = (int)math.round(bigNumber);
        return $"{bigNumberInt}%";
    }

    public static int StringToInt(this string s)
    {
        int o = 0;
        for (int i = 0; i < s.Length; i++)
        {
            unchecked
            {
                o += (int)s[i];
            }
        }
        return o;
    }

    public static string FormatCamelCase(string inString)
    {
        if (inString == null)
            return null;
        StringBuilder nameBuilder = new StringBuilder();
        bool doOnce = true;
        bool blockNextSpace = false;
        for (int i = 0; i < inString.Length; i++)
        {
            if (doOnce)
            {
                nameBuilder.Append(char.ToUpper(inString[i]));
                doOnce = false;
                blockNextSpace = true;//Capital Letters block spaces so "NPC" doesn't become "N PC".
            }
            else if (inString[i] == ' ' || !char.IsLetter(inString[i]))
            {
                blockNextSpace = true;//Block the next space so strings like "Hello.World" don't become "Hello..World"
                nameBuilder.Append(inString[i]);
            }
            else if (char.IsUpper(inString[i]))
            {
                if (!blockNextSpace)
                {
                    nameBuilder.Append(' ');
                }
                nameBuilder.Append(char.ToUpper(inString[i]));
                blockNextSpace = true;//Capital Letters block spaces so "NPC" doesn't become "N P C".
            }
            else
            {
                nameBuilder.Append(inString[i]);
                blockNextSpace = false;
            }
        }
        return nameBuilder.ToString();
    }

    /// <summary>
    /// Makes a string bold for display in a client-side TMP
    /// </summary>
    /// <param name="message">The string to embolden</param>
    /// <returns></returns>
    public static string Bold(string message)
    {
        return $"<b>{message}</b>";
    }

    /// <summary>
    /// Makes a string italics for display in a client-side TMP
    /// </summary>
    /// <param name="message">The string to italicise</param>
    /// <returns></returns>
    public static string Italic(string message)
    {
        return $"<i>{message}</i>";
    }

    /// <summary>
    /// Makes a string underlined for display in a client-side TMP
    /// </summary>
    /// <param name="message">The string to underline</param>
    /// <returns></returns>
    public static string Underline(string message)
    {
        return $"<u>{message}</u>";
    }

    /// <summary>
    /// Makes a string strikethrough for display in a client-side TMP
    /// </summary>
    /// <param name="message">The string to strike</param>
    /// <returns></returns>
    public static string Strikethrough(string message)
    {
        return $"<s>{message}</s>";
    }

    /// <summary>
    /// Makes a string coloured for display in a client-side TMP
    /// </summary>
    /// <param name="message">The string to color</param>
    /// <param name="color">The colour to make the string</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Color(this string message, Color color)
    {
        Color32 c32 = color;
        return $"<color=#{c32.r.ToString("X2") + c32.g.ToString("X2") + c32.b.ToString("X2")}>{message}</color>";
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Color(this object message, Color color) => message?.ToString().Color(color);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Size(this string message, int size)
    {
        return $"<size={size}>{message}</size>";
    }
        
    /// <summary>
    /// Formats 71.05 into 71'.05' (where '' is resized to decimalSize)
    /// </summary>
    /// <param name="val"></param>
    /// <param name="decimalSize"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString_ResizeDecimals(this float val, int decimalSize) => ToString_ResizeDecimals(val, null, decimalSize);
    /// <summary>
    /// Formats 71.05 into "71"'.05' (where "" is mainSize and '' is resized to decimalSize)
    /// </summary>
    /// <param name="val"></param>
    /// <param name="decimalSize"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString_ResizeDecimals(this float val, int? mainSize, int decimalSize)
    {
        int intVal = (int)math.floor(val);
        float decVal = math.frac(val);
            
        if (mainSize.HasValue)
            return $"<size={mainSize.Value}>{intVal}</size><size={decimalSize}>{decVal.ToString("N2").Substring(1)}</size>";
        else
            return $"{intVal}<size={decimalSize}>{decVal.ToString("N2").Substring(1)}</size>";
    }


    /// <summary>
    /// Create an image tag for display in a client-side TMP
    /// </summary>
    /// <param name="imageIndex">The index of the image in the sprite array. See Dialogue Window's main text for an example</param>
    /// <returns></returns>
    public static string Image(int imageIndex)
    {
        return $"<sprite={imageIndex}>";
    }

    public static string AOrAnForWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return "a";

        var firstLetter = char.ToLower(word[0]);
        switch (firstLetter)
        {
            case 'a':
            case 'e':
            case 'i':
            case 'o':
            case 'u'://Sometimes U uses 'a' instead of 'an' but this function is too simple to care.
                return "an";

            default:
                return "a";
        }
    }

    public static string TimeFrom24Hour(int hour24)
    {
        return $"{hour24:00}:00";
    }

    public static string? FirstCharToLowerCase(this string? str)
    {
        if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
            return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

        return str;
    }

    public static string? FirstCharToUpperCase(this string? str)
    {
        if (!string.IsNullOrEmpty(str) && char.IsLower(str[0]))
            return str.Length == 1 ? char.ToUpper(str[0]).ToString() : char.ToUpper(str[0]) + str[1..];

        return str;
    }
        
    public static string Substring_Safe(this string? str, int start, int length)
    {
        if (str == null) return str;
        int safeStart = math.min(start, str.Length);
        int safeLength = math.min(length, str.Length - start);
        return str?.Substring(safeStart, safeLength);
    }
}