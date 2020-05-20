using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PostInitializer : ACardInitializer<PostData>
{
    [SerializeField] private int trustImpact = 10;
    // [SerializeField] private int contaminationImpact = 10;
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private TextMeshProUGUI authorName = null;
    [SerializeField] private Image avatar = null;

    public override void Init(PostData data)
    {
        base.Init(data);
        text.SetText(data.Text);
        authorName.SetText(data.AuthorName);
        avatar.sprite = data.Avatar;
        GetComponent<CardDisplayer>().Display(data);
    }

    public override void AffectPopulation()
    {
        if(data.HasImpact)
        {
            AffectTrust(GetComponent<Reportable>().IsReported);
            // AffectContamination();
        }
    }

    private void AffectTrust(bool isReported)
    {
        if(isReported)
        {
            PopulationHandler.TrustLevel += data.IsFake ? trustImpact : -trustImpact;
        }
        else
        {
            PopulationHandler.TrustLevel += data.IsFake ? -trustImpact : trustImpact;
        }
    }
}
