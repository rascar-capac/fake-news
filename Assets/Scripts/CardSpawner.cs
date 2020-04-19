using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private List<PostData> _postDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minPostInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxPostInterval = 5f;
    [SerializeField] private PostHandler postPrefab = null;
    [SerializeField] private RectTransform postContext = null;
    [SerializeField] private List<EventData> _eventDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minEventInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxEventInterval = 5f;
    [SerializeField] private EventHandler eventPrefab = null;
    [SerializeField] private RectTransform eventContext = null;

    public List<PostData> PostDeck { get => _postDeck ; }
    public List<EventData> EventDeck { get => _eventDeck ; }

    private float postTimer;
    private float eventTimer;
    private PopulationHandler populationHandler;

    private void Awake()
    {
        populationHandler = FindObjectOfType<PopulationHandler>();
    }

    private void Start()
    {
        postTimer = Random.Range(minPostInterval, maxPostInterval);
        eventTimer = Random.Range(minEventInterval, maxEventInterval);
    }

    private void Update()
    {
        postTimer -= Time.deltaTime;
        if(postTimer <= 0)
        {
            if(PostDeck.Count > 0)
            {
                PostHandler newPost = Instantiate(postPrefab, postContext);
                newPost.transform.SetAsFirstSibling();
                PostData post = PostDeck[Random.Range(0, PostDeck.Count)];
                PostDeck.Remove(post);
                newPost.Init(post, populationHandler);

                foreach(PostData postData in post.postsToAdd)
                {
                    if(!PostDeck.Contains(postData))
                    {
                        PostDeck.Add(postData);
                    }
                }
                foreach(EventData eventData in post.eventsToAdd)
                {
                    if(!EventDeck.Contains(eventData))
                    {
                        EventDeck.Add(eventData);
                    }
                }
            }
            postTimer = Random.Range(minPostInterval, maxPostInterval);
        }

        eventTimer -= Time.deltaTime;
        if(eventTimer <= 0)
        {
            if(EventDeck.Count > 0)
            {
                EventHandler newEvent = Instantiate(eventPrefab, eventContext);
                newEvent.transform.SetAsFirstSibling();
                EventData eventData = EventDeck[Random.Range(0, EventDeck.Count)];
                EventDeck.Remove(eventData);
                newEvent.Init(eventData, populationHandler);

                foreach(PostData postData in eventData.postsToAdd)
                {
                    if(!PostDeck.Contains(postData))
                    {
                        PostDeck.Add(postData);
                    }
                }
                foreach(EventData eventDataToAdd in eventData.eventsToAdd)
                {
                    if(!EventDeck.Contains(eventDataToAdd))
                    {
                        EventDeck.Add(eventDataToAdd);
                    }
                }
            }
            eventTimer = Random.Range(minEventInterval, maxEventInterval);
        }
    }
}
