using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSpawner : ASpawner<InfoInitializer, InfoData>
{
    [SerializeField] private SpawnableInfo infoCard = null;
    [SerializeField] private TextAsset infos = null;
    private TimeHandler timeHandler;
    private PostSpawner postSpawner;

    private void Awake()
    {
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
            infoCard.AddData(newData);
        }

        timeHandler.OnTimeChanged.AddListener(TestSpawnProbability);
    }

    private void TestSpawnProbability()
    {
        if(infoCard.DataDeck.Count > 0)
        {
            if(Random.value <= infoCard.SpawnProbability)
            {
                SpawnCard();
            }
        }
    }

    private void SpawnCard()
    {
        InfoInitializer newCard = base.SpawnObject(infoCard);
        if(newCard.Data.HasImpact)
        {
            newCard.AffectTrust();
        }
        postSpawner.AddRelatedPosts(newCard.Data);
    }

    private InfoData CreateInfo(string template, Dictionary<string, string[]> categoryElements, Dictionary<int, string> tags)
    {
        string[] templateElements = template.Split(new char[]{' '}, 4);
        InfoData newData = ScriptableObject.CreateInstance<InfoData>();
        newData.Code = templateElements[0];
        newData.IsAffirmative = templateElements[1] == "+" ? true : false;
        newData.HasImpact = templateElements[2] == "I" ? true : false;
        newData.Text = FileReader.Format(templateElements[3], categoryElements);
        int tagCode = int.Parse(newData.Code.Substring(0, 1));
        newData.Tag = tags[tagCode];
        return newData;
    }

    [System.Serializable]
    protected class SpawnableInfo : SpawnableObject
    {
        public float SpawnProbability => spawnProbability;

        [SerializeField] [Range(0, 1f)] private float spawnProbability = 0.1f;
    }
}
