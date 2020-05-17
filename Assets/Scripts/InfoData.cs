using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoData : ACardData {
    public string Tag => tag;

    private string tag;

    public InfoData(string text, string code, bool isAffirmative, bool hasImpact, string tag) :
            base(text, code, isAffirmative, hasImpact)
    {
        this.tag = tag;
    }
}
