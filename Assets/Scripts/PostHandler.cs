using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostHandler : ACardHandler
{
    [SerializeField] private Color publishedColor = Color.gray;
    [SerializeField] private int expirationDelay = 5;

    private Image background;

    public void Start()
    {
        StartCoroutine(Expire());
        background = GetComponent<Image>();
    }

    public void ValidatePost()
    {
        AffectPopulation();
        // Destroy(this.gameObject);
        background.color = publishedColor;
    }

    public void BlockPost()
    {
        // Destroy(this.gameObject);
        background.color = publishedColor;
    }

    public IEnumerator Expire()
    {
        yield return new WaitForSeconds(expirationDelay);
        ValidatePost();
    }
}
