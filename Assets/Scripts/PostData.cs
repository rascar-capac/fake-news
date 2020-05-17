using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Post", menuName = "Post")]
public class PostData : ACardData {
    public bool IsFake { get; set; }
}
