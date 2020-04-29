using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trust = null;
    [SerializeField] private TextMeshProUGUI paranoia = null;
    [SerializeField] private TextMeshProUGUI contamination = null;
    [SerializeField] private TextMeshProUGUI casualties = null;
    [SerializeField] private TextMeshProUGUI time = null;
    [SerializeField] private TextMeshProUGUI gameOverTitle = null;
    [SerializeField] private TextMeshProUGUI finalScore = null;
    [SerializeField] private Color updateColor = Color.red;
    [SerializeField] private string gameLostText = "Population décimée";
    [SerializeField] private string gameWonText = "Épidémie surmontée";
    private Color initialFontColor;
    private GameManager gameManager;
    private TimeHandler timeHandler;
    private PopulationHandler populationHandler;

    private void Awake()
    {
        initialFontColor = trust.color;
        gameManager = GetComponent<GameManager>();
        timeHandler = GetComponent<TimeHandler>();
        populationHandler = GetComponent<PopulationHandler>();
    }

    private void Start()
    {
        gameManager.OnGameEnded.AddListener(UpdateGameOverScreen);
        timeHandler.OnTimeChanged.AddListener(UpdateTime);
        populationHandler.OnTrustLevelChanged.AddListener(UpdateTrustLevel);
        populationHandler.OnParanoiaLevelChanged.AddListener(UpdateParanoiaLevel);
        populationHandler.OnContaminationLevelChanged.AddListener(UpdateContaminationLevel);
        populationHandler.OnCasualtiesCountChanged.AddListener(UpdateCasualtiesCount);
    }

    private void UpdateTrustLevel()
    {
        trust.color = updateColor;
        trust.fontSize += 2;
        LeanTween.value(trust.gameObject, trust.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => trust.color = value);
        trust.SetText(populationHandler.TrustLevel.ToString());
    }

    private void UpdateParanoiaLevel()
    {
        paranoia.color = updateColor;
        paranoia.fontSize += 2;
        LeanTween.value(paranoia.gameObject, paranoia.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => paranoia.color = value);
        paranoia.SetText(populationHandler.ParanoiaLevel.ToString());
    }

    private void UpdateContaminationLevel()
    {
        contamination.color = updateColor;
        contamination.fontSize += 2;
        LeanTween.value(contamination.gameObject, contamination.color, initialFontColor, 2f)
                .setOnUpdateColor((Color value) => contamination.color = value);
        contamination.SetText(populationHandler.ContaminationLevel.ToString());
    }

    private void UpdateCasualtiesCount()
    {
        casualties.color = updateColor;
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

    private void UpdateGameOverScreen(bool isGameLost, int finalScore)
    {
        this.gameOverTitle.SetText(isGameLost ? gameLostText : gameWonText);
        this.finalScore.SetText(finalScore.ToString());
    }
}
