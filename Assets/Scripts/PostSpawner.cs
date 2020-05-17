using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSpawner : ASpawner<PostInitializer, PostData>
{
    [SerializeField] private TextAsset posts = null;
    [SerializeField] [Range(0, 1f)] private float postSpawnProbability = 0.1f;
    private TimeHandler timeHandler;
    private List<PostData> data;

    public void AddRelatedPosts(InfoData infoData)
    {
        foreach(PostData postData in data)
        {
            if(postData.Code == infoData.Code)
            {
                AddData(postData);
                if(postData.IsAffirmative != infoData.IsAffirmative)
                {
                    postData.IsFake = true;
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        timeHandler = GetComponent<TimeHandler>();
        data = new List<PostData>();
    }

    private void Start()
    {
        Dictionary<string, string[]> categoryElements = FileReader.FetchCategories(posts.text);
        string[] templates = categoryElements["template"];
        for(int i = 0 ; i < templates.Length ; i++)
        {
            PostData newData = CreatePost(templates[i], categoryElements);
            data.Add(newData);
        }

        foreach(PostData postData in data)
        {
            if(postData.Code == "00")
            {
                AddData(postData);
            }
        }
        timeHandler.OnTimeChanged.AddListener(TestSpawnProbability);
    }

    private void TestSpawnProbability()
    {
        if(dataDeck.Count > 0)
        {
            if(Random.value <= postSpawnProbability)
            {
                SpawnCard();
            }
        }
    }

    private void SpawnCard()
    {
        base.SpawnObject();
    }

    private PostData CreatePost(string template, Dictionary<string, string[]> categoryElements)
    {
         string[] templateElements = template.Split(new char[]{' '}, 4);
         string code = templateElements[0];
         bool isAffirmative = templateElements[1] == "+" ? true : false;
         bool hasImpact = templateElements[2] == "I" ? true : false;
         string text = FileReader.Format(templateElements[3], categoryElements);
         bool isFake = false;
         PostData newData = new PostData(text, code, isAffirmative, hasImpact, isFake);
         return newData;
    }
}
