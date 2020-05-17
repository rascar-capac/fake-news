using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSpawner : ASpawner<InfoInitializer, InfoData>
{
    [SerializeField] private TextAsset infos = null;
    [SerializeField] [Range(0, 1f)] private float spawnProbability = 0.1f;
    private TimeHandler timeHandler;
    private PostSpawner postSpawner;

    protected override void Awake()
    {
        base.Awake();
        timeHandler = GetComponent<TimeHandler>();
        postSpawner = GetComponent<PostSpawner>();
    }

    private void Start()
    {
        Dictionary<string, string[]> categoryElements = FileReader.FetchCategories(infos.text);
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

        timeHandler.OnTimeChanged.AddListener(TestSpawnProbability);
    }

    private void TestSpawnProbability()
    {
        if(dataDeck.Count > 0)
        {
            if(Random.value <= spawnProbability)
            {
                SpawnCard();
            }
        }
    }

    private void SpawnCard()
    {
        InfoInitializer newCard = base.SpawnObject();
        if(newCard.Data.HasImpact)
        {
            newCard.AffectTrust();
        }
        postSpawner.AddRelatedPosts(newCard.Data);
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
}
