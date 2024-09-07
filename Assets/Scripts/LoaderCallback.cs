using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoaderCallback : MonoBehaviour
{
    [SerializeField]
    private Slider loadingSlider;

    [Space(13)]    
    
    [SerializeField] 
    private Image background;
    [SerializeField] 
    private Sprite[] backgroundSprites;

    [Space(13)]
    
    [SerializeField]
    private TextMeshProUGUI adviceText;
    [SerializeField]
    private string[] advices;

    private float timeLoading = 2f;
    private float timeLeft;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        Sprite randomSprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];
        background.sprite = randomSprite;

        adviceText.text = advices[Random.Range(0, advices.Length)];
    }

    private void Update()
    {
        if (timeLeft < timeLoading)
        {
            timeLeft += Time.deltaTime;
            loadingSlider.value = timeLeft;
        }
        else
        {
            Loader.LoaderCallback();
        }
    }
}
