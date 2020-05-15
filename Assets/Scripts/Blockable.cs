using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Blockable : MonoBehaviour
{
    [SerializeField] private int expirationDelay = 5;
    [SerializeField] private Color expiredColor = Color.gray;
    private CardInitializer cardInitializer;
    private Button cardButton;
    private IEnumerator expirationCoroutine;
    private bool isProcessed;

    public void Awake()
    {
        cardInitializer = GetComponent<CardInitializer>();
        cardButton = GetComponent<Button>();
        expirationCoroutine = WaitForExpiration();
        StartCoroutine(expirationCoroutine);
        isProcessed = false;
    }

    public void BlockPost()
    {
        if(!isProcessed)
        {
            cardInitializer.AffectTrust(true);
            cardButton.interactable = false;
            StopCoroutine(expirationCoroutine);
            isProcessed = true;
        }
    }

    public IEnumerator WaitForExpiration()
    {
        yield return new WaitForSeconds(expirationDelay);
        ValidatePost();
    }

    public void ValidatePost()
    {
        // cardInitializer.AffectContamination();
        cardInitializer.AffectTrust(false);
        ColorBlock expiredColors = cardButton.colors;
        expiredColors.disabledColor = expiredColor;
        cardButton.colors = expiredColors;
        cardButton.interactable = false;
        isProcessed = true;
    }
}
