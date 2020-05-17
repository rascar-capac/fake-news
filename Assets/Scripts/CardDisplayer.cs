using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardDisplayer : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float timeToSlide = .5f;
    [SerializeField] private TextMeshProUGUI text = null;

    public void Display(ACardData data, System.Action callback)
    {
        text.SetText(data.Text);
        transform.SetAsFirstSibling();
        GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(SlideSmooth(callback));
    }

    private IEnumerator SlideSmooth(System.Action callback)
    {
        yield return null;
        LayoutElement layoutElement = GetComponent<LayoutElement>();
        LeanTween.value(0, GetComponent<LayoutGroup>().preferredHeight, timeToSlide).setEaseOutQuint()
                .setOnUpdate((float value) => layoutElement.preferredHeight = value)
                .setOnComplete(callback);
    }
}
