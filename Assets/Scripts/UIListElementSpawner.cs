using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListElementSpawner : MonoBehaviour
{
    [SerializeField] [Range(0, float.MaxValue)] private float minInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxInterval = 5f;
    [SerializeField] private GameObject elementPrefab = null;
    [SerializeField] private RectTransform context = null;

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
            Instantiate(elementPrefab, context);
            timer = Random.Range(minInterval, maxInterval);
        }
    }
}
