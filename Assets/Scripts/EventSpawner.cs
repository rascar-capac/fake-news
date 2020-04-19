using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField] private List<EventData> _events = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxInterval = 5f;
    [SerializeField] private EventInitializer eventPrefab = null;
    [SerializeField] private RectTransform context = null;
    [SerializeField] private PostSpawner postSpawner = null;

    public List<EventData> Events { get => _events; }

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
            EventInitializer newEvent = Instantiate(eventPrefab, context);
            newEvent.transform.SetAsFirstSibling();
            newEvent.Data = Events[Random.Range(0, Events.Count)];
            postSpawner.Posts.AddRange(newEvent.Data.posts);
            newEvent.Init();
            timer = Random.Range(minInterval, maxInterval);
        }
    }
}
