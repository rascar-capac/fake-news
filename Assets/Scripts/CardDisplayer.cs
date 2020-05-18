using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float timeToSlide = .5f;
    [SerializeField] [Range(0, 10f)] private float timeToAppear = .5f;
    [SerializeField] private TextMeshProUGUI text = null;

    public void Display(ACardData data, bool isNewCard = true, System.Action callback = null)
    {
        if(isNewCard)
        {
            text?.SetText(data.Text);
        }
        transform.SetAsFirstSibling();
        GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(SlideSmooth(callback, true));
    }

    public void Hide(System.Action callback = null)
    {
        StartCoroutine(SlideSmooth(callback, false));
    }

    private IEnumerator SlideSmooth(System.Action callback, bool isAppearing)
    {
        yield return null; // required to have preferredHeight set

        if(isAppearing)
        {
            float from = 0;
            float to = GetComponent<LayoutGroup>().preferredHeight;
            LeanTween.value(from , to, timeToSlide).setEaseOutQuint()
                    .setOnUpdate((float value) => GetComponent<LayoutElement>().preferredHeight = value)
                    .setOnComplete(() => { FadeCard(true); callback?.Invoke(); });
        }
        else
        {
            float from = GetComponent<LayoutGroup>().preferredHeight;
            float to = 0;
            FadeCard(false).setOnComplete(() =>
                    LeanTween.value(from , to, timeToSlide).setEaseOutQuint()
                            .setOnUpdate((float value) => GetComponent<LayoutElement>().preferredHeight = value)
                            .setOnComplete(() => { callback?.Invoke(); }));
        }
    }

    private LTDescr FadeCard(bool isAppearing)
    {
        return GetComponent<CanvasGroup>().LeanAlpha(isAppearing ? 1 : 0, timeToAppear).setEaseOutQuint();
    }
}
