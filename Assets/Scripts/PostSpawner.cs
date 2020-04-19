using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSpawner : MonoBehaviour
{
    [SerializeField] private List<PostData> _posts = null;
    [SerializeField] [Range(0, float.MaxValue)] private float minInterval = 0.2f;
    [SerializeField] [Range(0, float.MaxValue)] private float maxInterval = 5f;
    [SerializeField] private PostInitializer postPrefab = null;
    [SerializeField] private RectTransform context = null;

    public List<PostData> Posts { get => _posts ; }

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
            PostInitializer newPost = Instantiate(postPrefab, context);
            newPost.Data = Posts[Random.Range(0, Posts.Count)];
            newPost.Init();
            timer = Random.Range(minInterval, maxInterval);
        }
    }
}
