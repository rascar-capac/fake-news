using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventData : AData
{
    public int trust = 0;
    public int paranoia = 0;
    public int contamination = 0;
    [Range(0, int.MaxValue)] public int casualties = 0;
}
