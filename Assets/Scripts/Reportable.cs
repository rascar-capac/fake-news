using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Reportable : MonoBehaviour
{
    [SerializeField] private int expirationDelay = 5;
    [SerializeField] private Color expiredColor = Color.gray;
    private PostInitializer postInitializer;
    private Button cardButton;
    private IEnumerator expirationCoroutine;
    private bool isProcessed;

    public void ValidatePost()
    {
        // cardInitializer.AffectContamination();
        if(postInitializer.Data.HasImpact)
        {
            postInitializer.AffectTrust(false);
        }
        ColorBlock expiredColors = cardButton.colors;
        expiredColors.disabledColor = expiredColor;
        cardButton.colors = expiredColors;
        cardButton.interactable = false;
        isProcessed = true;
    }

    public void ReportPost()
    {
        if(!isProcessed)
        {
            if(postInitializer.Data.HasImpact)
            {
                postInitializer.AffectTrust(true);
            }
            cardButton.interactable = false;
            StopCoroutine(expirationCoroutine);
            isProcessed = true;
        }
    }

    private void Awake()
    {
        postInitializer = GetComponent<PostInitializer>();
        cardButton = GetComponent<Button>();
        expirationCoroutine = WaitForExpiration();
        StartCoroutine(expirationCoroutine);
        isProcessed = false;
    }

    private IEnumerator WaitForExpiration()
    {
        yield return new WaitForSeconds(expirationDelay);
        ValidatePost();
    }
}
