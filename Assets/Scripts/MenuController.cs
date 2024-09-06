using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Toggle effectsToggle;
    [SerializeField] 
    private Toggle musicToggle;

    [Space(13)]
    
    [SerializeField] 
    private AudioSource musicSource;    
    [SerializeField] 
    private AudioSource effectSource;

    [Space(13)] 
    
    [SerializeField] 
    private GameObject levelsPanel;
    [SerializeField] 
    private GameObject settingsPanel;
    [SerializeField] 
    private GameObject exitPanel;
    [SerializeField] 
    private GameObject mainPanel;
    
    private void Awake()
    {
        InitSettings();
    }

    public void OnStart(int level)
    {
        SceneManager.LoadScene("Level" + level);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)) 
        {
            VolumeController.Instance.PlaySound(VolumeController.Sounds.UIClick);
        }
    }

    private void InitSettings()
    {
        Time.timeScale = 1;    
        int effects = PlayerPrefs.GetInt("Effects");
        switch (effects) 
        {
            case 0:
                effectsToggle.isOn = false;
                effectSource.mute = false;
                break;
            case 1:
                effectsToggle.isOn = true;
                effectSource.mute = true;
                break;
        }
        int music = PlayerPrefs.GetInt("Music");
        switch (music)
        {
            case 0:
                musicToggle.isOn = false;
                musicSource.mute = false;
                break;
            case 1:
                musicToggle.isOn = true;
                musicSource.mute = true;
                break;
        }
    }

    public void SetEffects()
    {
        int num = effectsToggle.isOn == true ? 1 : 0;
        effectSource.mute = num == 1 ? true : false;
        PlayerPrefs.SetInt("Effects", num);
        Debug.Log("Effects is " + num);
    }

    public void SetMusic()
    {
        int num = musicToggle.isOn == true ? 1 : 0;
        musicSource.mute = num == 1 ? true : false;
        PlayerPrefs.SetInt("Music", num);
        Debug.Log("Music is " + num);
    }

    public void OpenLevelsPanel()
    {
        levelsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void CloseLevelsPanel()
    {
        levelsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenSettingsButton()
    {
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void CloseSettingsButton()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenExitPanel()
    {
        mainPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void CloseExitPanel()
    {
        mainPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
