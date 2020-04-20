using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private List<ACardData> _postDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minPostInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxPostInterval = 5f;
    [SerializeField] private PostHandler postPrefab = null;
    [SerializeField] private RectTransform postContext = null;
    [SerializeField] private List<ACardData> _eventDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minEventInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxEventInterval = 5f;
    [SerializeField] private EventHandler eventPrefab = null;
    [SerializeField] private RectTransform eventContext = null;

    public List<ACardData> PostDeck { get => _postDeck ; }
    public List<ACardData> EventDeck { get => _eventDeck ; }

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
                SpawnCard(PostDeck, postPrefab, postContext);
            }
            postTimer = Random.Range(minPostInterval, maxPostInterval);
        }

        eventTimer -= Time.deltaTime;
        if(eventTimer <= 0)
        {
            if(EventDeck.Count > 0)
            {
                SpawnCard(EventDeck, eventPrefab, eventContext);
            }
            eventTimer = Random.Range(minEventInterval, maxEventInterval);
        }
    }

    private void SpawnCard(List<ACardData> deck, ACardHandler prefab, RectTransform context)
    {
        ACardHandler newCard = Instantiate(prefab, context);
        newCard.transform.SetAsFirstSibling();
        ACardData data = deck[Random.Range(0, deck.Count)];
        deck.Remove(data);
        newCard.Init(data, populationHandler);

        foreach(PostData postData in data.postsToAdd)
        {
            if(!PostDeck.Contains(postData))
            {
                PostDeck.Add(postData);
            }
        }
        foreach(EventData eventData in data.eventsToAdd)
        {
            if(!EventDeck.Contains(eventData))
            {
                EventDeck.Add(eventData);
            }
        }
    }
}
