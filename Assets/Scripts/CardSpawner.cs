using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : ASpawner<CardInitializer, ACardData>
{
    [SerializeField] private SpawnableCard postCard = null;
    [SerializeField] private SpawnableCard infoCard = null;
    private TimeHandler timeHandler;

    private void Awake()
    {
        timeHandler = GetComponent<TimeHandler>();
    }

    private void Start()
    {
        timeHandler.OnTimeChanged.AddListener(TestSpawnProbabilities);
    }

    private void TestSpawnProbabilities()
    {
        TestSpawnProbability(postCard);
        TestSpawnProbability(infoCard);
    }

    private void TestSpawnProbability(SpawnableCard spawnableCard)
    {
        if(spawnableCard.DataDeck.Count > 0)
        {
            if(Random.value <= spawnableCard.CardSpawnProbability)
            {
                SpawnObject(spawnableCard);
            }
        }
    }

    protected override CardInitializer SpawnObject(SpawnableObject spawnableCard)
    {
        CardInitializer newCard = base.SpawnObject(spawnableCard);

        foreach(PostData postData in newCard.Data.PostsToAdd)
        {
            if(!postCard.DataDeck.Contains(postData))
            {
                postCard.DataDeck.Add(postData);
            }
        }
        foreach(InfoData infoData in newCard.Data.InfosToAdd)
        {
            if(!infoCard.DataDeck.Contains(infoData))
            {
                infoCard.DataDeck.Add(infoData);
            }
        }

        return newCard;
    }

    [System.Serializable]
    protected class SpawnableCard : SpawnableObject
    {
        [SerializeField] [Range(0, 1f)] private float cardSpawnProbability = 0.1f;

        public float CardSpawnProbability => cardSpawnProbability;
    }
}
