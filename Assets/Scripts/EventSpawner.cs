using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField] private List<EventData> _eventDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxInterval = 5f;
    [SerializeField] private EventInitializer eventPrefab = null;
    [SerializeField] private RectTransform context = null;
    [SerializeField] private PostSpawner postSpawner = null;

    public List<EventData> EventDeck { get => _eventDeck; }

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
            if(EventDeck.Count > 0)
            {
                EventInitializer newEvent = Instantiate(eventPrefab, context);
                newEvent.transform.SetAsFirstSibling();
                EventData data = EventDeck[Random.Range(0, EventDeck.Count)];
                EventDeck.Remove(data);
                newEvent.Data = data;

                foreach(EventData eventData in newEvent.Data.eventsToAdd)
                {
                    if(!EventDeck.Contains(eventData))
                    {
                        EventDeck.Add(eventData);
                    }
                }
                foreach(PostData postData in newEvent.Data.postsToAdd)
                {
                    if(!postSpawner.PostDeck.Contains(postData))
                    {
                        postSpawner.PostDeck.Add(postData);
                    }
                }

                newEvent.Init();
            }

            timer = Random.Range(minInterval, maxInterval);
        }
    }
}
