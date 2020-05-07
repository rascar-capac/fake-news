using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    [SerializeField] private TextAsset posts = null;
    [SerializeField] private TextAsset infos = null;
    private List<ACardData> postData;
    private List<ACardData> infoData;

    public List<ACardData> PostData => postData;
    public List<ACardData> InfoData => infoData;

    private void Awake()
    {
        postData = new List<ACardData>();
        Dictionary<string, string[]> categories = FetchCategories(posts.text);
        string[] templates = categories["template"];
        for(int i = 0 ; i < templates.Length ; i++)
        {
            postData.Add(CreateData(templates[i], categories, posts.text));
        }

        infoData = new List<ACardData>();
        categories = FetchCategories(infos.text);
        templates = categories["template"];
        for(int i = 0 ; i < templates.Length ; i+= 2)
        {
            string template = templates[i + Random.Range(0, 2)];
            infoData.Add(CreateData(template, categories, infos.text));
        }
    }

    private Dictionary<string, string[]> FetchCategories(string text)
    {
        Dictionary<string, string[]> categoryElements = new Dictionary<string, string[]>();
        string[] categories = FetchCategory("categories", text);
        foreach(string category in categories)
        {
            categoryElements.Add(category, FetchCategory(category, text));
        }
        return categoryElements;
    }

    private string[] FetchCategory(string categoryName, string text)
    {
        string categoryText = GetTextBetween($"#{categoryName}\n", "\n\n", text);
        return categoryText.Split('\n');
    }

    private ACardData CreateData(string template, Dictionary<string, string[]> categories, string text)
    {
        string[] content = template.Split(new char[]{' '}, 4);
        ACardData newData = ScriptableObject.CreateInstance<ACardData>();
        newData.Code = content[0];
        newData.IsAffirmative = content[1] == "+" ? true : false;
        newData.Tag = content[2];
        newData.Text = Format(content[3], categories);
        return newData;
    }

    private string Format(string rawString, Dictionary<string, string[]> categoryElements)
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

    private string GetTextBetween(string start, string end, string text)
    {
        int startIndex = text.IndexOf(start) + start.Length;
        int length = text.Substring(startIndex).IndexOf(end);
        return text.Substring(startIndex, length);
    }

    private string ReplaceTextBetween(string start, string end, string replacement, string text)
    {
        int startIndex = text.IndexOf(start);
        int length = text.Substring(startIndex).IndexOf(end, start.Length) + end.Length;
        string textToReplace = text.Substring(startIndex, length);
        return text.Replace(textToReplace, replacement);
    }

    private string PickRandom(string categoryName, Dictionary<string, string[]> categoryElements)
    {
        string[] elements = categoryElements[categoryName];
        return elements[Random.Range(0, elements.Length)];
    }

    private string PickRandom(string[] options)
    {
        return options[Random.Range(0, options.Length)];
    }
}
