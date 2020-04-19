using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ACardHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;

    public ACardData Data { get; set; }

    private PopulationHandler populationHandler;

    public virtual void Init(ACardData data, PopulationHandler populationHandler)
    {
        Data = data;
        this.populationHandler = populationHandler;
        text.SetText(Data.text);
    }

    public virtual void AffectPopulation()
    {
        populationHandler.TrustLevel = Mathf.Clamp(populationHandler.TrustLevel + Data.trust, 0, int.MaxValue);
        populationHandler.ParanoiaLevel = Mathf.Clamp(populationHandler.ParanoiaLevel + Data.paranoia, 0, int.MaxValue);
        populationHandler.ContaminationLevel = Mathf.Clamp(populationHandler.ContaminationLevel + Data.contamination, 0, int.MaxValue);
        populationHandler.CasualtiesCount = Mathf.Clamp(populationHandler.CasualtiesCount + Data.casualties, 0, int.MaxValue);
    }
}
