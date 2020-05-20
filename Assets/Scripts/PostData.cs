using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostData : ACardData {
    public bool IsFake { get; set; }
    public string AuthorName => authorName;
    public Sprite Avatar => avatar;

    private string authorName;
    private Sprite avatar;

    public PostData(string text, string code, bool isAffirmative, bool hasImpact, bool isFake, string authorName, Sprite avatar) :
            base(text, code, isAffirmative, hasImpact)
    {
        IsFake = isFake;
        this.authorName = authorName;
        this.avatar = avatar;
    }
}
