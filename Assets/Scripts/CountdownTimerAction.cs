using UnityEngine;
using TMPro;

public class CountdownTimerAction : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int time;
    public bool autoStart;

    void Start()
    {
        if(this.autoStart)
            StartTimer(this.time);
    }

    public void StartTimer(int time)
    {
        timerText.enabled = true;
        this.time = time;
        InvokeRepeating("ChangeTimer", 0, 1);
    }

    void ChangeTimer()
    {
        time -= 1;
        timerText.text = time.ToString("F0");

        if (this.time <= 0)
        {
            CancelInvoke("ChangeTimer");
            timerText.enabled = false;
        }
    }
}