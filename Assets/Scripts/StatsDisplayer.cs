using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trustDisplay = null;
    [SerializeField] private TextMeshProUGUI paranoiaDisplay = null;
    [SerializeField] private TextMeshProUGUI contaminationDisplay = null;
    [SerializeField] private TextMeshProUGUI casualtiesDisplay = null;
    private PopulationHandler populationHandler = null;

    private void Awake()
    {
        populationHandler = GetComponent<PopulationHandler>();
    }

    private void Update()
    {
        trustDisplay.SetText("Trust level " + populationHandler.TrustLevel);
        paranoiaDisplay.SetText("Paranoia level " + populationHandler.ParanoiaLevel);
        contaminationDisplay.SetText("Contamination level " + populationHandler.ContaminationLevel);
        casualtiesDisplay.SetText("Casualties count " + populationHandler.CasualtiesCount);
    }
}
