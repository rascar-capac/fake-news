using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoInitializer : ADataInitializer<InfoData>
{
    [SerializeField] [Range(0, 10f)] private float timeToAppear = .5f;
    [SerializeField] private int trustImpact = 10;
    // [SerializeField] private int contaminationImpact = 10;
    private PopulationHandler populationHandler;
    private CardDisplayer cardDisplayer;

    public override void Init(InfoData data)
    {
        base.Init(data);
        cardDisplayer.Display(data, FinalizeCard);
    }

    public void AffectTrust()
    {
        populationHandler.TrustLevel += trustImpact;
    }

    // public void AffectContamination()
    // {
    //     populationHandler.ContaminationLevel += contaminationImpact;
    // }

    protected override void Awake()
    {
        base.Awake();
        populationHandler = FindObjectOfType<PopulationHandler>();
        cardDisplayer = GetComponent<CardDisplayer>();
    }

    private void FinalizeCard()
    {
        GetComponent<CanvasGroup>().LeanAlpha(1, timeToAppear).setEaseOutQuint();
        // ApplyInstantEffect();
    }

    // private void ApplyInstantEffect()
    // {
    //     AffectContamination();
    // }
}
