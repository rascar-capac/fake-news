using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextGenerator
{
    public static Dictionary<string, string[]> FetchCategories(string text)
    {
        Dictionary<string, string[]> categoryElements = new Dictionary<string, string[]>();
        string[] categoryNames = FetchCategory("categories", text);
        foreach(string categoryName in categoryNames)
        {
            categoryElements.Add(categoryName, FetchCategory(categoryName, text));
        }
        return categoryElements;
    }

    public static string Format(string rawString, Dictionary<string, string[]> categoryElements)
    {
        while(rawString.Contains("["))
        {
            string optionalElement = GetTextBetween("[", "]", rawString);
            rawString = ReplaceTextBetween("[", "]", Random.value > 0.5f ? optionalElement : "", rawString);
        }

        while(rawString.Contains("§"))
        {
            string placeholder = GetTextBetween("§", "§", rawString);
            rawString = ReplaceTextBetween("§", "§", PickRandom(placeholder, categoryElements), rawString);
        }

        while(rawString.Contains("("))
        {
            string[] options = GetTextBetween("(", ")", rawString).Split('-');
            rawString = ReplaceTextBetween("(", ")", PickRandom(options), rawString);
        }

        char[] strongPunctuation = new char[] {'.', '!', '?'};
        int strongPunctuationIndex = rawString.IndexOfAny(strongPunctuation);
        bool isEndOfString = false;
        while(strongPunctuationIndex > 0 && !isEndOfString)
        {
            bool isCharFound = false;
            while(!isCharFound && !isEndOfString)
            {
                strongPunctuationIndex++;
                isEndOfString = strongPunctuationIndex >= rawString.Length;
                if(!isEndOfString && rawString[strongPunctuationIndex] != ' ')
                {
                    rawString =
                            rawString.Substring(0, strongPunctuationIndex) +
                            rawString[strongPunctuationIndex].ToString().ToUpper() +
                            rawString.Substring(strongPunctuationIndex + 1);
                    isCharFound = true;
                }
            }
            if(!isEndOfString)
            {
                strongPunctuationIndex = rawString.IndexOfAny(strongPunctuation, strongPunctuationIndex);
            }
        }

        rawString = rawString[0].ToString().ToUpper() + rawString.Substring(1);

        return rawString;
    }

    private static string[] FetchCategory(string categoryName, string text)
    {
        string categoryText = GetTextBetween($"#{categoryName}\n", "\n\n", text);
        return categoryText.Split('\n');
    }

    private static string GetTextBetween(string start, string end, string text)
    {
        int startIndex = text.IndexOf(start) + start.Length;
        int length = text.Substring(startIndex).IndexOf(end);
        return text.Substring(startIndex, length);
    }

    private static string ReplaceTextBetween(string start, string end, string replacement, string text)
    {
        int startIndex = text.IndexOf(start);
        int length = text.Substring(startIndex).IndexOf(end, start.Length) + end.Length;
        string textToReplace = text.Substring(startIndex, length);
        return text.Replace(textToReplace, replacement);
    }

    private static string PickRandom(string categoryName, Dictionary<string, string[]> categoryElements)
    {
        string[] elements = categoryElements[categoryName];
        return elements[Random.Range(0, elements.Length)];
    }

    private static string PickRandom(string[] options)
    {
        return options[Random.Range(0, options.Length)];
    }
}
