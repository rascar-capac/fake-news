using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostInitializer : ACardInitializer<PostData>
{
    public override void AffectTrust(bool isReported)
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
