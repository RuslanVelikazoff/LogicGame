using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Toggle effectsToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Button[] Levels;

    [SerializeField] private AudioSource musicSource;    
    [SerializeField] private AudioSource effectSource;
    private void Awake()
    {
        InitSettings();
    }

    public void OnStart(int level)
    {
        SceneManager.LoadScene(level);
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
}
