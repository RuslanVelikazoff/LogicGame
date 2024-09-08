using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    [Header("Pause Panel")] 
    [SerializeField] 
    private Button pauseButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField] 
    private Button menuPauseButton;
    [SerializeField] 
    private GameObject pausePanel;

    [Space(13)] 
    
    [Header("Lose Panel")]
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private Button restartButton;
    [SerializeField] 
    private Button menuLoseButton;

    [Space(13)] 
    
    [Header("Win Panel")] 
    [SerializeField]
    private GameObject winPanel;
    [SerializeField] 
    private Button nextLevelButton;
    [SerializeField] 
    private Button menuWinButton;
    [SerializeField] 
    private TextMeshProUGUI timerText;
    
    [Space(13)]

    [SerializeField]
    private Slider scoreSlider;

    [SerializeField] 
    int scoreToWin, currentScore, numToBreak, maxBreak;

    public static GameUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PausePanelButtonClickAction();

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

    private void PausePanelButtonClickAction()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(() =>
            {
                OnPause();
            });
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                OnResume();
            });
        }

        if (menuPauseButton != null)
        {
            menuPauseButton.onClick.RemoveAllListeners();
            menuPauseButton.onClick.AddListener(() =>
            {
                OnMainMenu();
            });
        }
    }

    private void LosePanelButtonClickAction()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            { 
                OnRestart();  
            });
        }

        if (menuLoseButton != null)
        {
            menuLoseButton.onClick.RemoveAllListeners();
            menuLoseButton.onClick.AddListener(() =>
            {
                OnMainMenu();
            });
        }
    }

    private void WinPanelButtonClickAction()
    {
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(() =>
            {
                OnNextLevel();
            });
        }

        if (menuWinButton != null)
        {
            menuWinButton.onClick.RemoveAllListeners();
            menuWinButton.onClick.AddListener(() =>
            {
                OnMainMenu();
            });
        }
    }

    public IEnumerator OnWin()
    {
        if(Time.timeScale == 1)
        {
            timerText.text = TimerPanel.Instance.GetTimerText();
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 2)
            {
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                nextLevelButton.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.5f);
            winPanel.SetActive(true);
            VolumeController.Instance.PlaySound(VolumeController.Sounds.LevelComplete);
            
            yield return new WaitForSeconds(1f);
            
            Time.timeScale = 0;
            
            WinPanelButtonClickAction();
            yield return null;
        }
    }

    public IEnumerator OnLoseGame()
    {
        if (Time.timeScale == 1)
        {
            losePanel.SetActive(true);
            LosePanelButtonClickAction();
            VolumeController.Instance.PlaySound(VolumeController.Sounds.Lose);
            
            yield return new WaitForSeconds(1f);
            
            Time.timeScale = 0;
            
            yield return null;
        }
    }

    public IEnumerator OnPauseGame()
    {
        pausePanel.SetActive(true);
        
        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;
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
        StartCoroutine(OnLoseGame());
    }

    public void WaitOnWin()
    {
        StartCoroutine(OnWin());
    }

    private void OnRestart()
    {
        Time.timeScale = 1;
        var currentScene = SceneManager.GetActiveScene();
        Loader.Load(currentScene.name);
    } 
    
    private void OnNextLevel()
    {
        Time.timeScale = 1;
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Loader.Load("Level" + nextLevelIndex);
    }
    private void OnMainMenu()
    {
        Time.timeScale = 0;
        Loader.Load("Menu");
    }

    private void OnPause()
    {
        StartCoroutine(OnPauseGame());
    }

    private void OnResume()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }
}
