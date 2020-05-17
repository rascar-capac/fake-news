using System.Collections;
using System.Collections.Generic;

public abstract class ACardData
{
    public string Text => text;
    public string Code => code;
    public bool IsAffirmative => isAffirmative;
    public bool HasImpact => hasImpact;

    private string text;
    private string code;
    private bool isAffirmative;
    private bool hasImpact;

    public ACardData(string text, string code, bool isAffirmative, bool hasImpact)
    {
        this.text = text;
        this.code = code;
        this.isAffirmative = isAffirmative;
        this.hasImpact = hasImpact;
    }
}
