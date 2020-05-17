using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACardInitializer<T> : ADataInitializer<T> where T : ACardData
{
    public PopulationHandler PopulationHandler { get; set; }

    [SerializeField] [Range(0, 10f)] private float timeToAppear = .5f;
    [SerializeField] protected int trustImpact = 10;
    // [SerializeField] private int contaminationImpact = 10;
    private CardDisplayer cardDisplayer;

    public override void Init(T data)
    {
        base.Init(data);
        cardDisplayer.Display(data, FinalizeCard);
    }

    public abstract void AffectTrust(bool isBlocked);

    // public void AffectContamination()
    // {
    //     populationHandler.ContaminationLevel += contaminationImpact;
    // }

    protected override void Awake()
    {
        base.Awake();
        cardDisplayer = GetComponent<CardDisplayer>();
    }

    protected virtual void FinalizeCard()
    {
        GetComponent<CanvasGroup>().LeanAlpha(1, timeToAppear).setEaseOutQuint();
    }
}
