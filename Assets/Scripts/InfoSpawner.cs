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
        string[] templates = categoryElements["template"];
        for(int i = 0 ; i < templates.Length ; i += 2)
        {
            string template = templates[i + Random.Range(0, 2)];
            InfoData newData = CreateInfo(template, categoryElements);
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
        postSpawner.AddRelatedPosts(newCard.Data);
    }

    private InfoData CreateInfo(string template, Dictionary<string, string[]> categoryElements)
    {
        string[] content = template.Split(new char[]{' '}, 4);
        InfoData newData = ScriptableObject.CreateInstance<InfoData>();
        newData.Code = content[0];
        newData.IsAffirmative = content[1] == "+" ? true : false;
        newData.Tag = content[2];
        newData.Text = FileReader.Format(content[3], categoryElements);
        return newData;
    }

    [System.Serializable]
    protected class SpawnableInfo : SpawnableObject
    {
        public float SpawnProbability => spawnProbability;

        [SerializeField] [Range(0, 1f)] private float spawnProbability = 0.1f;
    }
}
