using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeHandler timeHandler = null;
    [SerializeField] private PopulationHandler populationHandler = null;
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject PauseMenuPanel = null;
    [SerializeField] private GameObject GameOverPanel = null;
    private UnityEvent _onGameStarted;
    private UnityEvent _onGameEnded;
    private bool _isGameRunning;
    private int _finalScore;
    private bool _isGameLost;

    public UnityEvent OnGameEnded => _onGameEnded;
    public bool IsGameRunning => _isGameRunning;
    public int FinalScore => _finalScore;
    public bool IsGameLost => _isGameLost;

    private void Awake()
    {
        _onGameStarted = new UnityEvent();
        _onGameEnded = new UnityEvent();
        _isGameRunning = false;
        _isGameLost = false;
    }

    private void Start()
    {
        timeHandler.OnLastDayReached.AddListener(EndGame);
        populationHandler.OnFullContamination.AddListener(LoseGame);
        GameOverPanel.SetActive(false);
        PauseMenuPanel.SetActive(false);
    }

    public void StartGame()
    {
        _isGameRunning = true;
        MainMenuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Debug.Log("pause");
        _isGameRunning = false;
        PauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        _isGameRunning = true;
        PauseMenuPanel.SetActive(false);
    }

    private void LoseGame()
    {
        _isGameLost = true;
        EndGame();
    }

    private void EndGame()
    {
        _isGameRunning = false;
        OnGameEnded.Invoke();
        GameOverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    private void ComputeFinalScore()
    {
        _finalScore = 0;
    }
}
