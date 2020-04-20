using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private PopulationHandler populationHandler = null;
    [SerializeField] private TextMeshProUGUI trustDisplay = null;
    [SerializeField] private TextMeshProUGUI paranoiaDisplay = null;
    [SerializeField] private TextMeshProUGUI contaminationDisplay = null;
    [SerializeField] private TextMeshProUGUI casualtiesDisplay = null;
    [SerializeField] private TimeHandler timeHandler = null;
    [SerializeField] private TextMeshProUGUI timeDisplay = null;

    private void Start()
    {
        populationHandler.OnTrustLevelChanged.AddListener(UpdateTrustLevel);
        populationHandler.OnParanoiaLevelChanged.AddListener(UpdateParanoiaLevel);
        populationHandler.OnContaminationLevelChanged.AddListener(UpdateContaminationLevel);
        populationHandler.OnCasualtiesCountChanged.AddListener(UpdateCasualtiesCount);
        timeHandler.OnTimeChanged.AddListener(UpdateTime);
    }

    private void UpdateTrustLevel()
    {
        trustDisplay.SetText(populationHandler.TrustLevel.ToString());
    }

    private void UpdateParanoiaLevel()
    {
        paranoiaDisplay.SetText(populationHandler.ParanoiaLevel.ToString());
    }

    private void UpdateContaminationLevel()
    {
        contaminationDisplay.SetText(populationHandler.ContaminationLevel.ToString());
    }

    private void UpdateCasualtiesCount()
    {
        casualtiesDisplay.SetText(populationHandler.CasualtiesCount.ToString());
    }

    private void UpdateTime()
    {
        timeDisplay.SetText(timeHandler.DayCount.ToString());
    }
}
