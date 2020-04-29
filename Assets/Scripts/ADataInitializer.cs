using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ADataInitializer<T> : MonoBehaviour where T : ScriptableObject
{
    protected T data;
    public class DataInitializedEvent : UnityEvent<T> {}
    private DataInitializedEvent onDataInitialized;

    public T Data => data;
    public DataInitializedEvent OnDataInitialized => onDataInitialized;

    protected virtual void Awake()
    {
        onDataInitialized = new DataInitializedEvent();
    }

    public virtual void Init(T data)
    {
        this.data = data;
        onDataInitialized.Invoke(data);
    }
}
