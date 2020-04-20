using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameDuration = 100;
    [SerializeField] private TimeHandler timeHandler = null;
    [SerializeField] private PopulationHandler populationHandler = null;
    [SerializeField] private RectTransform GameOverPanel = null;
    private UnityEvent _onGameEnded;
    private int _finalScore;
    private bool _isGameLost;

    public UnityEvent OnGameEnded => _onGameEnded;
    public int FinalScore => _finalScore;
    public bool IsGameLost => _isGameLost;

    private void Awake()
    {
        _onGameEnded = new UnityEvent();
        _isGameLost = false;
    }

    private void Start()
    {
        timeHandler.OnLastDayReached.AddListener(EndGame);
        populationHandler.OnFullContamination.AddListener(LoseGame);
        GameOverPanel.gameObject.SetActive(false);
    }

    private void LoseGame()
    {
        _isGameLost = true;
        EndGame();
    }

    private void EndGame()
    {
        OnGameEnded.Invoke();
        GameOverPanel.gameObject.SetActive(true);
    }

    private void ComputeFinalScore()
    {
        _finalScore = 0;
    }
}
