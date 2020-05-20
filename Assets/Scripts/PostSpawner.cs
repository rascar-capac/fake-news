using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSpawner : ACardSpawner<PostInitializer, PostData>
{
    [SerializeField] TextAsset rawNameData = null;
    [SerializeField] List<Sprite> avatars = null;
    [SerializeField] RectTransform postedArea = null;
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
        data = new List<PostData>();
    }

    protected override void InstantiateTemplates(Dictionary<string, string[]> categoryElements)
    {
        Dictionary<string, string[]> nameCategoryElements = TextGenerator.FetchCategories(rawNameData.text);
        string[] templates = categoryElements["template"];

        for(int i = 0 ; i < templates.Length ; i++)
        {
            PostData newData = CreatePost(templates[i], categoryElements, nameCategoryElements);
            data.Add(newData);
        }

        foreach(PostData postData in data)
        {
            if(postData.Code == "00")
            {
                AddData(postData);
            }
        }
    }

    private PostData CreatePost(string template, Dictionary<string, string[]> categoryElements, Dictionary<string, string[]> nameCategoryElements)
    {
         string[] templateElements = template.Split(new char[]{' '}, 4);
         string code = templateElements[0];
         bool isAffirmative = templateElements[1] == "+" ? true : false;
         bool hasImpact = templateElements[2] == "I" ? true : false;
         string text = TextGenerator.Format(templateElements[3], categoryElements);
         bool isFake = false;
         string[] nameTemplates = nameCategoryElements["template"];
         string rawAuthorName = nameTemplates[Random.Range(0, nameTemplates.Length)];
         string authorName = TextGenerator.Format(rawAuthorName, nameCategoryElements);
         Sprite avatar = avatars[Random.Range(0, avatars.Count)];
         PostData newData = new PostData(text, code, isAffirmative, hasImpact, isFake, authorName, avatar);
         return newData;
    }

    protected override PostInitializer SpawnObject()
    {
        PostInitializer newPost = base.SpawnObject();
        Postable postable;
        if(newPost.TryGetComponent<Postable>(out postable))
        {
            postable.PostedArea = postedArea;
        }
        return newPost;
    }
}
