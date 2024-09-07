using TMPro;
using UnityEngine;

public class TimerPanel : MonoBehaviour
{
    public static TimerPanel Instance;

    private bool startTimer = true;
    
    [SerializeField] 
    private TextMeshProUGUI timerText;

    private int minute;
    private float seconds;

    private string minuteText;
    private string secondsText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (startTimer)
        {
            seconds += Time.deltaTime;

            if (seconds >= 60)
            {
                minute++;
                seconds = 0;
            }

            SetTimerText();
        }
    }

    private void SetTimerText()
    {
        if (minute < 10)
        {
            minuteText = "0" + minute;
        }
        else
        {
            minuteText = minute.ToString();
        }

        if (seconds < 10)
        {
            secondsText = "0" + (int)seconds;
        }
        else
        {
            secondsText = ((int) seconds).ToString();
        }

        timerText.text = minuteText + ":" + secondsText;
    }

    public string GetTimerText()
    {
        startTimer = false;
        return minuteText + ":" + secondsText;
    }
}
