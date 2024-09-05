using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public AudioClip[] audioClips;

    [Header("AudioSources")]
    public AudioSource EffectSource;
    [SerializeField] private AudioSource MusicSource;

    public static VolumeController Instance;
    public enum Sounds
    {
        UIClick,
        Lose,
        LevelComplete
    }
    public Sounds soundState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitSounds();
    }

    public void InitSounds()
    {
        int effects = PlayerPrefs.GetInt("Effects");
        switch (effects)
        {
            case 0:
                EffectSource.mute = false;
                break;
            case 1:
                EffectSource.mute = true;
                break;
        }
        int music = PlayerPrefs.GetInt("Music");
        switch (music)
        {
            case 0:
                MusicSource.mute = false;
                break;
            case 1:
                MusicSource.mute = true;
                break;
        }
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.UIClick:
                EffectSource.PlayOneShot(audioClips[0]);
                break;
            case Sounds.Lose:
                EffectSource.PlayOneShot(audioClips[1]);
                break;
            case Sounds.LevelComplete:
                EffectSource.PlayOneShot(audioClips[2]);
                break;                              
        }
    }
}
