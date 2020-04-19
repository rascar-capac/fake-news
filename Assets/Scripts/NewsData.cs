using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New News", menuName = "News")]
public class NewsData : AData
{
    public int paranoia = 0;
    public int contamination = 0;
    [Range(0, int.MaxValue)] public int casualties = 0;
}
