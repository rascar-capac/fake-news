using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSpawner : MonoBehaviour
{
    [SerializeField] private List<PostData> _postDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxInterval = 5f;
    [SerializeField] private PostInitializer postPrefab = null;
    [SerializeField] private RectTransform context = null;
    [SerializeField] private EventSpawner eventSpawner = null;

    public List<PostData> PostDeck { get => _postDeck ; }

    private float timer;

    private void Start()
    {
        timer = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(PostDeck.Count > 0)
            {
                PostInitializer newPost = Instantiate(postPrefab, context);
                newPost.transform.SetAsFirstSibling();
                PostData data = PostDeck[Random.Range(0, PostDeck.Count)];
                PostDeck.Remove(data);
                newPost.Data = data;

                foreach(PostData postData in newPost.Data.postsToAdd)
                {
                    if(!PostDeck.Contains(postData))
                    {
                        PostDeck.Add(postData);
                    }
                }
                foreach(EventData eventData in newPost.Data.eventsToAdd)
                {
                    if(!eventSpawner.EventDeck.Contains(eventData))
                    {
                        eventSpawner.EventDeck.Add(eventData);
                    }
                }

                newPost.Init();
            }

            timer = Random.Range(minInterval, maxInterval);
        }
    }
}
