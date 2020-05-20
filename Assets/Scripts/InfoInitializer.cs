using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoInitializer : ACardInitializer<InfoData>
{
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private int trustImpact = 10;
    // [SerializeField] private int contaminationImpact = 10;

    public override void Init(InfoData data)
    {
        base.Init(data);
        text.SetText(data.Text);
        GetComponent<CardDisplayer>().Display(data, AffectPopulation);
    }

    public override void AffectPopulation()
    {
        if(data.HasImpact)
        {
            AffectTrust();
            // AffectContamination();
        }
    }

    private void AffectTrust()
    {
        PopulationHandler.TrustLevel += trustImpact;
    }

    // private void AffectContamination()
    // {
    //     populationHandler.ContaminationLevel += contaminationImpact;
    // }
}
