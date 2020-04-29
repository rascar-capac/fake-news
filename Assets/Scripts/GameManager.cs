using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float menuTransitionsTime = 1f;
    [SerializeField] private CanvasGroup mainMenuPanel = null;
    [SerializeField] private CanvasGroup pauseMenuPanel = null;
    [SerializeField] private CanvasGroup gameOverPanel = null;
    private bool isGameRunning;
    private int finalScore;
    private bool isGameLost;
    private TimeHandler timeHandler;
    private PopulationHandler populationHandler;
    public class GameEndedEvent : UnityEvent<bool, int> {}
    private GameEndedEvent onGameEnded;

    public bool IsGameRunning => isGameRunning;
    public GameEndedEvent OnGameEnded => onGameEnded;

    private void Awake()
    {
        isGameRunning = false;
        isGameLost = false;
        timeHandler = GetComponent<TimeHandler>();
        populationHandler = GetComponent<PopulationHandler>();
        onGameEnded = new GameEndedEvent();
    }

    private void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        pauseMenuPanel.gameObject.SetActive(false);
        timeHandler.OnLastDayReached.AddListener(EndGame);
        populationHandler.OnFullContamination.AddListener(LoseGame);
    }

    public void StartGame()
    {
        isGameRunning = true;
        mainMenuPanel.interactable = false;
        mainMenuPanel.LeanAlpha(0, menuTransitionsTime)
                .setOnComplete(() => mainMenuPanel.gameObject.SetActive(false));
    }

    public void PauseGame()
    {
        isGameRunning = false;
        pauseMenuPanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        isGameRunning = true;
        pauseMenuPanel.gameObject.SetActive(false);
    }

    private void LoseGame()
    {
        isGameLost = true;
        EndGame();
    }

    private void EndGame()
    {
        isGameRunning = false;
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.interactable = false;
        gameOverPanel.alpha = 0;
        gameOverPanel.LeanAlpha(1, menuTransitionsTime)
                .setOnComplete(() => gameOverPanel.interactable = true);
        ComputeFinalScore();
        OnGameEnded.Invoke(isGameLost, finalScore);
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
        finalScore = 0;
    }
}
