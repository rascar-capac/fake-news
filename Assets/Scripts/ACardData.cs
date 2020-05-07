using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACardData : ScriptableObject
{
    private string text;
    private string code;
    private string tag;
    private bool isAffirmative;
    private int trust = 0;
    private int paranoia = 0;
    private int contamination = 0;
    [Range(0, 1000)] private int casualties = 0;

    public string Text { get; set; }
    public string Code { get; set; }
    public string Tag { get; set; }
    public bool IsAffirmative { get; set; }
    public int Trust => trust;
    public int Paranoia => paranoia;
    public int Contamination => contamination;
    public int Casualties => casualties;
}
