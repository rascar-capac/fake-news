using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostHandler : ACardHandler
{
    [SerializeField] private int expirationDelay = 5;
    [SerializeField] private Color expiredColor = Color.gray;
    private Button card;
    private IEnumerator expirationCoroutine;
    private bool isProcessed;

    public void Awake()
    {
        card = GetComponent<Button>();
        expirationCoroutine = Expire();
        StartCoroutine(expirationCoroutine);
        isProcessed = false;
    }

    public void BlockPost()
    {
        if(!isProcessed)
        {
            card.interactable = false;
            StopCoroutine(expirationCoroutine);
            isProcessed = true;
        }
    }

    public IEnumerator Expire()
    {
        yield return new WaitForSeconds(expirationDelay);
        ValidatePost();
    }

    public void ValidatePost()
    {
        AffectPopulation();
        ColorBlock expiredColors = card.colors;
        expiredColors.disabledColor = expiredColor;
        card.colors = expiredColors;
        card.interactable = false;
        isProcessed = true;
    }
}
