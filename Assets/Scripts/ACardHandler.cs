using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ACardHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;
    private ACardData data;
    private PopulationHandler populationHandler;

    public virtual void Init(ACardData data, PopulationHandler populationHandler)
    {
        this.data = data;
        this.populationHandler = populationHandler;
        text.SetText(data.text);
    }

    public virtual void AffectPopulation()
    {
        populationHandler.TrustLevel += data.trust;
        populationHandler.ParanoiaLevel += data.paranoia;
        populationHandler.ContaminationLevel += data.contamination;
        populationHandler.CasualtiesCount += data.casualties;
    }
}
