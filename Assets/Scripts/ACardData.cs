using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACardData : ScriptableObject
{
    [TextArea] public string text = null;
    public int trust = 0;
    public int paranoia = 0;
    public int contamination = 0;
    [Range(0, 1000)] public int casualties = 0;

    public List<PostData> postsToAdd = null;
    public List<EventData> eventsToAdd = null;
}
