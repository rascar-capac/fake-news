using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostHandler : ACardHandler
{
    public void Start()
    {
        StartCoroutine(Expire());
    }

    public void ValidatePost()
    {
        AffectPopulation();
        Destroy(this.gameObject);
    }

    public void BlockPost()
    {
        Destroy(this.gameObject);
    }

    public IEnumerator Expire()
    {
        yield return new WaitForSeconds(2);
        ValidatePost();
    }
}
