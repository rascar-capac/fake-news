using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Post", menuName = "Post")]
public class PostData : AData
{
    public int paranoia = 0;
    public int contamination = 0;
    [Range(0, int.MaxValue)] public int casualties = 0;
}
