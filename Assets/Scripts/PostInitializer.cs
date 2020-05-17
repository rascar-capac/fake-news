using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostInitializer : ACardInitializer<PostData>
{
    public override void AffectTrust(bool isBlocked)
    {
        if(isBlocked)
        {
            PopulationHandler.TrustLevel += data.IsFake ? trustImpact : -trustImpact;
        }
        else
        {
            PopulationHandler.TrustLevel += data.IsFake ? -trustImpact : trustImpact;
        }
    }
}
