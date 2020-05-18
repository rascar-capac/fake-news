using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostInitializer : ACardInitializer<PostData>
{
    [SerializeField] private int trustImpact = 10;
    // [SerializeField] private int contaminationImpact = 10;

    public override void Init(PostData data)
    {
        base.Init(data);
        GetComponent<CardDisplayer>().Display(data);
    }

    public override void AffectPopulation()
    {
        if(data.HasImpact)
        {
            AffectTrust(GetComponent<Reportable>().IsReported);
            // AffectContamination();
        }
    }

    private void AffectTrust(bool isReported)
    {
        if(isReported)
        {
            PopulationHandler.TrustLevel += data.IsFake ? trustImpact : -trustImpact;
        }
        else
        {
            PopulationHandler.TrustLevel += data.IsFake ? -trustImpact : trustImpact;
        }
    }
}
