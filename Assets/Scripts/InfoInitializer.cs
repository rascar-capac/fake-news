using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoInitializer : ACardInitializer<InfoData>
{
    public override void AffectTrust(bool isReported)
    {
        PopulationHandler.TrustLevel += trustImpact;
    }

    protected override void FinalizeCard()
    {
        base.FinalizeCard();
        if(data.HasImpact)
        {
            ApplyInstantEffect();
        }
    }

    private void ApplyInstantEffect()
    {
        AffectTrust(false);
        // AffectContamination();
    }
}
