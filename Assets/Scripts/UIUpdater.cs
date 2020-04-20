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

    private void Start()
    {
        populationHandler.OnTrustLevelChanged.AddListener(UpdateTrustLevel);
        populationHandler.OnParanoiaLevelChanged.AddListener(UpdateParanoiaLevel);
        populationHandler.OnContaminationLevelChanged.AddListener(UpdateContaminationLevel);
        populationHandler.OnCasualtiesCountChanged.AddListener(UpdateCasualtiesCount);
        timeHandler.OnTimeChanged.AddListener(UpdateTime);
        gameManager.OnGameEnded.AddListener(UpdateGameOverScreen);
    }

    private void UpdateTrustLevel()
    {
        trust.SetText(populationHandler.TrustLevel.ToString());
    }

    private void UpdateParanoiaLevel()
    {
        paranoia.SetText(populationHandler.ParanoiaLevel.ToString());
    }

    private void UpdateContaminationLevel()
    {
        contamination.SetText(populationHandler.ContaminationLevel.ToString());
    }

    private void UpdateCasualtiesCount()
    {
        casualties.SetText(populationHandler.CasualtiesCount.ToString());
    }

    private void UpdateTime()
    {
        time.SetText(timeHandler.DayCount.ToString());
    }

    private void UpdateGameOverScreen()
    {
        gameOverTitle.SetText(gameManager.IsGameLost ? "Population décimée" : "Épidémie surmontée");
        finalScore.SetText(gameManager.FinalScore.ToString());
    }
}
