using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACardSpawner<T, U> : ASpawner<T, U>
        where T : ACardInitializer<U>
        where U : ACardData
{
    [SerializeField] private TextAsset rawCardData = null;
    [SerializeField] [Range(0, 1f)] private float spawnProbability = 0.1f;
    private TimeHandler timeHandler;

    protected override void Awake()
    {
        base.Awake();
        timeHandler = GetComponent<TimeHandler>();
    }

    private void Start()
    {
        Dictionary<string, string[]> categoryElements = TextGenerator.FetchCategories(rawCardData.text);
        InstantiateTemplates(categoryElements);
        timeHandler.OnTimeChanged.AddListener(TestSpawnProbability);
    }

    protected abstract void InstantiateTemplates(Dictionary<string, string[]> categoryElements);

    protected void TestSpawnProbability()
    {
        if(dataDeck.Count > 0)
        {
            if(Random.value <= spawnProbability)
            {
                SpawnObject();
            }
        }
    }

    protected override T SpawnObject()
    {
        T newCard = base.SpawnObject();
        newCard.PopulationHandler = GetComponent<PopulationHandler>();
        return newCard;
    }
}
