using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInitializer : ADataInitializer<ACardData>
{
    [SerializeField] [Range(0, 10f)] private float timeToSpawn = 1f;
    [SerializeField] private int trustImpact = 10;
    [SerializeField] private int contaminationImpact = 10;
    [SerializeField] private bool hasInstantEffect = false;
    [SerializeField] private TextMeshProUGUI text = null;
    private bool isFake;
    private PopulationHandler populationHandler;

    protected override void Awake()
    {
        base.Awake();
        populationHandler = FindObjectOfType<PopulationHandler>();
    }

    public override void Init(ACardData data)
    {
        base.Init(data);

        if(hasInstantEffect)
        {
            AffectContamination();
        }

        Display();
    }

    public IEnumerator SlideSmooth()
    {
        yield return null;
        LayoutElement layoutElement = GetComponent<LayoutElement>();
        LeanTween.value(0, GetComponent<HorizontalLayoutGroup>().preferredHeight, timeToSpawn / 2).setEaseOutQuint()
                .setOnUpdate((float value) => layoutElement.preferredHeight = value)
                .setOnComplete(() => GetComponent<CanvasGroup>().LeanAlpha(1, timeToSpawn / 2).setEaseOutQuint());
    }

    public void AffectTrust(bool isBlocked)
    {
        if(isBlocked)
        {
            populationHandler.TrustLevel += data.IsFake ? trustImpact : -trustImpact;
        }
        else
        {
            populationHandler.TrustLevel += data.IsFake ? -trustImpact : trustImpact;
        }
    }

    public void AffectContamination()
    {
        populationHandler.ContaminationLevel += data.IsFake ? contaminationImpact : -contaminationImpact;
    }

    private void Display()
    {
        text.SetText(data.Text);
        transform.SetAsFirstSibling();
        GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(SlideSmooth());
    }
}
