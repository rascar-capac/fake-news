using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACardInitializer<T> : ADataInitializer<T> where T : ACardData
{
    public PopulationHandler PopulationHandler { get; set; }

    public abstract void AffectPopulation();
}
