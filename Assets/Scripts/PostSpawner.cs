using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSpawner : ASpawner<PostInitializer, PostData>
{
    [SerializeField] private SpawnablePost postCard = null;
    [SerializeField] private TextAsset posts = null;
    private TimeHandler timeHandler;
    private List<PostData> data;

    public void AddRelatedPosts(InfoData infoData)
    {
        foreach(PostData postData in data)
        {
            if(postData.Code == infoData.Code)
            {
                postCard.AddData(postData);
                if(postData.IsAffirmative != infoData.IsAffirmative)
                {
                    postData.IsFake = true;
                }
            }
        }
    }

    private void Awake()
    {
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
                postCard.AddData(postData);
            }
        }
        timeHandler.OnTimeChanged.AddListener(TestSpawnProbability);
    }

    private void TestSpawnProbability()
    {
        if(postCard.DataDeck.Count > 0)
        {
            if(Random.value <= postCard.PostSpawnProbability)
            {
                SpawnCard();
            }
        }
    }

    private void SpawnCard()
    {
        base.SpawnObject(postCard);
    }

    private PostData CreatePost(string template, Dictionary<string, string[]> categoryElements)
    {
        string[] templateElements = template.Split(new char[]{' '}, 4);
        PostData newData = ScriptableObject.CreateInstance<PostData>();
        newData.Code = templateElements[0];
        newData.IsAffirmative = templateElements[1] == "+" ? true : false;
        newData.HasImpact = templateElements[2] == "I" ? true : false;
        newData.Text = FileReader.Format(templateElements[3], categoryElements);
        newData.IsFake = false;
        return newData;
    }

    [System.Serializable]
    protected class SpawnablePost : SpawnableObject
    {
        public float PostSpawnProbability => postSpawnProbability;

        [SerializeField] [Range(0, 1f)] private float postSpawnProbability = 0.1f;
    }
}
