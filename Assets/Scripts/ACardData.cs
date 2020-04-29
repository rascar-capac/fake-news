using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACardData : ScriptableObject
{
    [SerializeField] [TextArea] private string text = null;
    [SerializeField] private int trust = 0;
    [SerializeField] private int paranoia = 0;
    [SerializeField] private int contamination = 0;
    [SerializeField] [Range(0, 1000)] private int casualties = 0;
    [SerializeField] private List<PostData> postsToAdd = null;
    [SerializeField] private List<InfoData> infosToAdd = null;

    public string Text => text;
    public int Trust => trust;
    public int Paranoia => paranoia;
    public int Contamination => contamination;
    public int Casualties => casualties;
    public List<PostData> PostsToAdd => postsToAdd;
    public List<InfoData> InfosToAdd => infosToAdd;
}
