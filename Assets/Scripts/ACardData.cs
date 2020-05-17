using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACardData : ScriptableObject
{
    public string Text { get; set; }
    public string Code { get; set; }
    public bool IsAffirmative { get; set; }
    public bool HasImpact { get; set; }
}
