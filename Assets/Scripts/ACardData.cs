using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACardData : ScriptableObject
{
    public string Text { get; set; }
    public string Code { get; set; }
    public string Tag { get; set; }
    public bool IsAffirmative { get; set; }
    public bool IsFake { get; set; }

    private string text;
    private string code;
    private string tag;
    private bool isAffirmative;
    private bool isFake;
}
