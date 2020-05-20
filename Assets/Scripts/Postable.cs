using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Postable : MonoBehaviour
{
    public RectTransform PostedArea { get; set; }

    [SerializeField] private int postDelay = 5;
    [SerializeField] private Color postedColor = Color.gray;
    private Button cardButton;
    private PostInitializer postInitializer;
    private CardDisplayer cardDisplayer;
    private Reportable reportable;
    private IEnumerator postingCoroutine;

    public void Post()
    {
        ColorBlock postedColors = cardButton.colors;
        postedColors.disabledColor = reportable.IsReported ? cardButton.colors.normalColor : postedColor;
        cardButton.colors = postedColors;
        cardButton.interactable = false;

        cardDisplayer.Hide(() => {
            transform.SetParent(PostedArea);
            cardDisplayer.Display(postInitializer.Data, postInitializer.AffectPopulation);
        });
    }

    private void Awake()
    {
        cardButton = GetComponent<Button>();
        postInitializer = GetComponent<PostInitializer>();
        cardDisplayer = GetComponent<CardDisplayer>();
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
