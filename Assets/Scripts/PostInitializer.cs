using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostInitializer : ADataInitializer<PostData>
{
    [SerializeField] [Range(0, 10f)] private float timeToAppear = .5f;
    [SerializeField] private int trustImpact = 10;
    // [SerializeField] private int contaminationImpact = 10;
    private PopulationHandler populationHandler;
    private CardDisplayer cardDisplayer;

    public override void Init(PostData data)
    {
        base.Init(data);
        cardDisplayer.Display(data, FinalizeCard);
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

    // public void AffectContamination()
    // {
    //     populationHandler.ContaminationLevel += data.IsFake ? contaminationImpact : -contaminationImpact;
    // }

    protected override void Awake()
    {
        base.Awake();
        populationHandler = FindObjectOfType<PopulationHandler>();
        cardDisplayer = GetComponent<CardDisplayer>();
    }

    private void FinalizeCard()
    {
        GetComponent<CanvasGroup>().LeanAlpha(1, timeToAppear / 2).setEaseOutQuint();
    }
}
