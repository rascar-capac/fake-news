using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSpawner : ACardSpawner<InfoInitializer, InfoData>
{
    private PostSpawner postSpawner;

    protected override void Awake()
    {
        base.Awake();
        postSpawner = GetComponent<PostSpawner>();
    }

    protected override void InstantiateTemplates(Dictionary<string, string[]> categoryElements)
    {
        Dictionary<int, string> tags = new Dictionary<int, string>();
        foreach(string rawTag in categoryElements["tag"])
        {
            string[] tagElements = rawTag.Split(' ');
            tags.Add(int.Parse(tagElements[0]), tagElements[1]);
        }

        string[] templates = categoryElements["template"];
        for(int i = 0 ; i < templates.Length ; i += 2)
        {
            string template = templates[i + Random.Range(0, 2)];
            InfoData newData = CreateInfo(template, categoryElements, tags);
            AddData(newData);
        }
    }

    private InfoData CreateInfo(string template, Dictionary<string, string[]> categoryElements, Dictionary<int, string> tags)
    {
        string[] templateElements = template.Split(new char[]{' '}, 4);
        string code = templateElements[0];
        bool isAffirmative = templateElements[1] == "+" ? true : false;
        bool hasImpact = templateElements[2] == "I" ? true : false;
        string text = FileReader.Format(templateElements[3], categoryElements);
        int tagCode = int.Parse(code.Substring(0, 1));
        string tag = tags[tagCode];
        InfoData newData = new InfoData(text, code, isAffirmative, hasImpact, tag);
        return newData;
    }

    protected override InfoInitializer SpawnObject()
    {
        InfoInitializer newCard = base.SpawnObject();
        postSpawner.AddRelatedPosts(newCard.Data);
        return newCard;
    }
}
