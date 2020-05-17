using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostData : ACardData {
    public bool IsFake { get; set; }

    public PostData(string text, string code, bool isAffirmative, bool hasImpact, bool isFake) :
            base(text, code, isAffirmative, hasImpact)
    {
        IsFake = isFake;
    }
}
