﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private List<ACardData> _postDeck = null;
    [SerializeField] private PostHandler postPrefab = null;
    [SerializeField] private RectTransform postContext = null;
    [SerializeField] [Range(0, 1f)] private float postSpawnProbability = 0.1f;
    [SerializeField] private List<ACardData> _infoDeck = null;
    [SerializeField] private InfoHandler infoPrefab = null;
    [SerializeField] private RectTransform infoContext = null;
    [SerializeField] [Range(0, 1f)] private float infoSpawnProbability = 0.1f;
    [SerializeField] private TimeHandler timeHandler = null;
    [SerializeField] private PopulationHandler populationHandler = null;

    public List<ACardData> PostDeck => _postDeck;
    public List<ACardData> InfoDeck => _infoDeck;

    private void Start()
    {
        timeHandler.OnTimeChanged.AddListener(TestSpawnProbability);
    }

    private void TestSpawnProbability()
    {
        if(PostDeck.Count > 0)
        {
            if(Random.value <= postSpawnProbability)
            {
                SpawnCard(PostDeck, postPrefab, postContext);
            }
        }
        if(InfoDeck.Count > 0)
        {
            if(Random.value <= infoSpawnProbability)
            {
                SpawnCard(InfoDeck, infoPrefab, infoContext);
            }
        }
    }

    private void SpawnCard(List<ACardData> deck, ACardHandler prefab, RectTransform context)
    {
        ACardHandler newCard = Instantiate(prefab, context);
        // newCard.transform.SetAsFirstSibling();
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
