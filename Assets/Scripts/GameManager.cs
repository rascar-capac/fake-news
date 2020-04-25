using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float menuTransitionsTime = 1f;
    [SerializeField] private TimeHandler timeHandler = null;
    [SerializeField] private PopulationHandler populationHandler = null;
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject PauseMenuPanel = null;
    [SerializeField] private GameObject GameOverPanel = null;
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
        CanvasGroup canvasGroup = MainMenuPanel.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.LeanAlpha(0, menuTransitionsTime)
                .setOnComplete(() => MainMenuPanel.SetActive(false));
    }

    public void PauseGame()
    {
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
        CanvasGroup canvasGroup = GameOverPanel.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        canvasGroup.LeanAlpha(1, menuTransitionsTime)
                .setOnComplete(() => canvasGroup.interactable = true);
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
