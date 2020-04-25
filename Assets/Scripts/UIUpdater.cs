using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private PopulationHandler populationHandler = null;
    [SerializeField] private TextMeshProUGUI trust = null;
    [SerializeField] private TextMeshProUGUI paranoia = null;
    [SerializeField] private TextMeshProUGUI contamination = null;
    [SerializeField] private TextMeshProUGUI casualties = null;
    [SerializeField] private TimeHandler timeHandler = null;
    [SerializeField] private TextMeshProUGUI time = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private TextMeshProUGUI gameOverTitle = null;
    [SerializeField] private TextMeshProUGUI finalScore = null;
    [SerializeField] private Color updateColor = Color.red;
    private Color initialFontColor;


    private void Start()
    {
        populationHandler.OnTrustLevelChanged.AddListener(UpdateTrustLevel);
        populationHandler.OnParanoiaLevelChanged.AddListener(UpdateParanoiaLevel);
        populationHandler.OnContaminationLevelChanged.AddListener(UpdateContaminationLevel);
        populationHandler.OnCasualtiesCountChanged.AddListener(UpdateCasualtiesCount);
        timeHandler.OnTimeChanged.AddListener(UpdateTime);
        gameManager.OnGameEnded.AddListener(UpdateGameOverScreen);
        initialFontColor = trust.color;
    }

    private void UpdateTrustLevel()
    {
        trust.color = Color.red;
        trust.fontSize += 2;
        LeanTween.value(trust.gameObject, trust.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => trust.color = value);
        trust.SetText(populationHandler.TrustLevel.ToString());
    }

    private void UpdateParanoiaLevel()
    {
        paranoia.color = Color.red;
        paranoia.fontSize += 2;
        LeanTween.value(paranoia.gameObject, paranoia.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => paranoia.color = value);
        paranoia.SetText(populationHandler.ParanoiaLevel.ToString());
    }

    private void UpdateContaminationLevel()
    {
        contamination.color = Color.red;
        contamination.fontSize += 2;
        LeanTween.value(contamination.gameObject, contamination.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => contamination.color = value);
        contamination.SetText(populationHandler.ContaminationLevel.ToString());
    }

    private void UpdateCasualtiesCount()
    {
        casualties.color = Color.red;
        LeanTween.value(casualties.gameObject, casualties.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => casualties.color = value);
        casualties.SetText(populationHandler.CasualtiesCount.ToString());
    }

    private void UpdateTime()
    {
        time.SetText(timeHandler.DayCount.ToString());
        if(timeHandler.DayCount >= 90)
        {
            time.color = updateColor;
        }
    }

    private void UpdateGameOverScreen()
    {
        gameOverTitle.SetText(gameManager.IsGameLost ? "Population décimée" : "Épidémie surmontée");
        finalScore.SetText(gameManager.FinalScore.ToString());
    }
}
