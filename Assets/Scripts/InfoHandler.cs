using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : ACardHandler
{
    public override void Init(ACardData data, PopulationHandler populationHandler)
    {
        base.Init(data, populationHandler);
        AffectPopulation();
    }
}
