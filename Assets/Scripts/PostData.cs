using System.Collections;
using System.Collections.Generic;

public class PostData : ACardData {
    public bool IsFake { get; set; }
    public string AuthorName => authorName;

    private string authorName;

    public PostData(string text, string code, bool isAffirmative, bool hasImpact, bool isFake, string authorName) :
            base(text, code, isAffirmative, hasImpact)
    {
        IsFake = isFake;
        this.authorName = authorName;
    }
}
