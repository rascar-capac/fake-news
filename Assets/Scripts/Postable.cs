using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Postable : MonoBehaviour
{
    public bool IsPosted => isPosted;

    [SerializeField] private int postDelay = 5;
    [SerializeField] private Color postedColor = Color.gray;
    private bool isPosted;
    private Button cardButton;
    private PostInitializer postInitializer;
    private Reportable reportable;
    private IEnumerator postingCoroutine;

    public void Post()
    {
        if(postInitializer.Data.HasImpact)
        {
            postInitializer.AffectTrust(reportable.IsReported);
            // postInitializer.AffectContamination();
        }
        ColorBlock postedColors = cardButton.colors;
        postedColors.disabledColor = reportable.IsReported ? cardButton.colors.normalColor : postedColor;
        cardButton.colors = postedColors;
        cardButton.interactable = false;
        isPosted = true;
    }

    private void Awake()
    {
        isPosted = false;
        cardButton = GetComponent<Button>();
        postInitializer = GetComponent<PostInitializer>();
        reportable = GetComponent<Reportable>();
        postingCoroutine = WaitForPosting();
        StartCoroutine(postingCoroutine);
    }

    private IEnumerator WaitForPosting()
    {
        yield return new WaitForSeconds(postDelay);
        Post();
    }
}
