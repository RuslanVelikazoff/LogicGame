using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TMP_Text finalText;

    [SerializeField] private Slider scoreSlider;

    [SerializeField] int scoreToWin, currentScore, numToBreak, maxBreak;

    public static GameUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        restartButton.onClick.AddListener(() => OnRestart());
        mainMenuButton.onClick.AddListener(() => OnMainMenu());
        resumeButton.onClick.AddListener(() => OnResume()); 
        pauseButton.onClick.AddListener(() => OnPause());
        nextLevelButton.onClick.AddListener(() => OnNextLevel());

        scoreSlider.maxValue = scoreToWin;
        scoreSlider.minValue = 0;
        numToBreak = 0;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            VolumeController.Instance.PlaySound(VolumeController.Sounds.UIClick);
        }
    }

    public IEnumerator OnWin(string text)
    {
        if(Time.timeScale == 1)
        {
            resumeButton.gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                Debug.Log(SceneManager.sceneCountInBuildSettings);
                Debug.Log(SceneManager.GetActiveScene().buildIndex);
                restartButton.gameObject.SetActive(false);
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {

                restartButton.gameObject.SetActive(true);
                nextLevelButton.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.5f);
            pausePanel.SetActive(true);
            VolumeController.Instance.PlaySound(VolumeController.Sounds.LevelComplete);
            Time.timeScale = 0;

            finalText.text = $"{text}";
            yield return null;
        }
    }

    public IEnumerator OnLoseGame(string text)
    {
        if (Time.timeScale == 1)
        {
            resumeButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            pausePanel.SetActive(true);
            VolumeController.Instance.PlaySound(VolumeController.Sounds.Lose);
            Time.timeScale = 0;

            finalText.text = $"{text}";
            yield return null;
        }
    }

    public void IncreaseBreakCount()
    {
        numToBreak++;
        if(maxBreak <= numToBreak)
        {
            FindObjectOfType<Player>().isRun = true;
        }
    }

    public void IncreaseScoreCount()
    {
        currentScore++;
        scoreSlider.value = currentScore;
        if(scoreToWin <= currentScore)
        {
            WaitOnWin();
        }
    }

    public void OnLose()
    {
        StartCoroutine(OnLoseGame("Try Again"));
    }

    public void WaitOnWin()
    {
        StartCoroutine(OnWin("Level Complete!"));
    }

    private void OnRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
    
    private void OnNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnMainMenu()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }

    private void OnPause()
    {
        pausePanel.gameObject.SetActive(true);
        nextLevelButton.gameObject .SetActive(false);
        finalText.text = "Pause";
        Time.timeScale = 0;
    }

    private void OnResume()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }
}
