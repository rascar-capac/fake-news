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
    [SerializeField] private List<ACardData> _infoDeck = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minInfoInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxInfoInterval = 5f;
    [SerializeField] private InfoHandler infoPrefab = null;
    [SerializeField] private RectTransform infoContext = null;
    private PopulationHandler populationHandler;
    private float postTimer;
    private float infoTimer;

    public List<ACardData> PostDeck { get => _postDeck ; }
    public List<ACardData> InfoDeck { get => _infoDeck ; }

    private void Awake()
    {
        populationHandler = FindObjectOfType<PopulationHandler>();
    }

    private void Start()
    {
        postTimer = Random.Range(minPostInterval, maxPostInterval);
        infoTimer = Random.Range(minInfoInterval, maxInfoInterval);
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

        infoTimer -= Time.deltaTime;
        if(infoTimer <= 0)
        {
            if(InfoDeck.Count > 0)
            {
                SpawnCard(InfoDeck, infoPrefab, infoContext);
            }
            infoTimer = Random.Range(minInfoInterval, maxInfoInterval);
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
        foreach(InfoData infoData in data.infosToAdd)
        {
            if(!InfoDeck.Contains(infoData))
            {
                InfoDeck.Add(infoData);
            }
        }
    }
}
