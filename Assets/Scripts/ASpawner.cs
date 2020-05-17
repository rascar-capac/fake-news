using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpawner<T, U> : MonoBehaviour
        where T : ADataInitializer<U>
{
    public List<T> SpawnedObjects => spawnedObjects;

    [SerializeField] private T prefab = null;
    [SerializeField] private Transform context = null;
    [SerializeField] private bool hasUniqueData = false;
    protected List<U> dataDeck = new List<U>();
    protected List<T> spawnedObjects;

    public void AddData(U newData)
    {
        dataDeck.Add(newData);
    }

    public void AddData(List<U> newData)
    {
        dataDeck.AddRange(newData);
    }

    protected virtual void Awake()
    {
        dataDeck = new List<U>();
        spawnedObjects = new List<T>();
    }

    protected T SpawnObject()
    {
        T newObject = Instantiate(prefab, context);
        U data = PickRandomData(dataDeck, hasUniqueData);
        newObject.Init(data);
        spawnedObjects.Add(newObject);

        return newObject;
    }

    private U PickRandomData(List<U> deck, bool IsUnique)
    {
        U data = deck[Random.Range(0, deck.Count)];
        if(IsUnique)
        {
            deck.Remove(data);
        }
        return data;
    }
}
