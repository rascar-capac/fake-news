using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : ASpawner<CardInitializer, ACardData>
{
    [SerializeField] private SpawnableCard postCard = null;
    [SerializeField] private SpawnableCard infoCard = null;
    private TimeHandler timeHandler;
    private FileReader fileReader;

    private void Awake()
    {
        timeHandler = GetComponent<TimeHandler>();
        fileReader = GetComponent<FileReader>();
    }

    private void Start()
    {
        infoCard.AddData(fileReader.InfoData);
        foreach(ACardData cardData in fileReader.PostData)
        {
            if(cardData.Code == "0")
            {
                postCard.AddData(cardData);
            }
        }
        timeHandler.OnTimeChanged.AddListener(TestSpawnProbabilities);
    }

    private void TestSpawnProbabilities()
    {
        TestSpawnProbability(postCard, false);
        TestSpawnProbability(infoCard, true);
    }

    private void TestSpawnProbability(SpawnableCard spawnableCard, bool mustAddNewCards)
    {
        if(spawnableCard.DataDeck.Count > 0)
        {
            if(Random.value <= spawnableCard.CardSpawnProbability)
            {
                SpawnCard(spawnableCard, mustAddNewCards);
            }
        }
    }

    private CardInitializer SpawnCard(SpawnableCard spawnableCard, bool mustAddNewCards)
    {
        CardInitializer newCard = base.SpawnObject(spawnableCard);
        if(mustAddNewCards)
        {
            foreach(ACardData cardData in fileReader.PostData)
            {
                if(cardData.Code == newCard.Data.Code)
                {
                    postCard.AddData(cardData);
                    if(cardData.IsAffirmative != newCard.Data.IsAffirmative)
                    {
                        cardData.IsFake = true;
                    }
                }
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
