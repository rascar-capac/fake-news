using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpawner<T, U> : MonoBehaviour
        where T : ADataInitializer<U>
        where U : ScriptableObject
{
    protected virtual T SpawnObject(SpawnableObject spawnableObject)
    {
        T newObject = Instantiate(spawnableObject.Prefab, spawnableObject.Context);
        U data = PickRandomData(spawnableObject.DataDeck, spawnableObject.HasUniqueData);
        newObject.Init(data);
        spawnableObject.SpawnedObjects.Add(newObject);

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

    [System.Serializable]
    protected abstract class SpawnableObject
    {
        [SerializeField] private T prefab = null;
        [SerializeField] private Transform context = null;
        [SerializeField] private List<U> dataDeck = null;
        [SerializeField] private bool hasUniqueData = false;
        private List<T> spawnedObjects = new List<T>();

        public T Prefab => prefab;
        public Transform Context => context;
        public List<U> DataDeck => dataDeck;
        public bool HasUniqueData => hasUniqueData;
        public List<T> SpawnedObjects => spawnedObjects;
    }
}
