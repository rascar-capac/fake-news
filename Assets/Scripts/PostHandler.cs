using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostHandler : ACardHandler
{
    public void Start()
    {
        StartCoroutine(Expire());
    }

    public override void AffectPopulation()
    {
        base.AffectPopulation();
        Destroy(this.gameObject);
    }

    public IEnumerator Expire()
    {
        yield return new WaitForSeconds(2);
        AffectPopulation();
    }
}
