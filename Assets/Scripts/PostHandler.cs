using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostHandler : ACardHandler
{
    public override void AffectPopulation()
    {
        base.AffectPopulation();
        Destroy(this.gameObject);
    }
}
