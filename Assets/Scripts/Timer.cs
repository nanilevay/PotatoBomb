using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    float randomNumber;
    public int MinuteMargin = 5;
    public bool timerActive = false;
    public float currentTime;
    public float startMinutes;
    public TextMeshProUGUI currentTimeText;
    public MenuManager MenuSwitcher;

    public Image TimerImg;

    public float InitialSecs;

    public GameManager Manager;

    public void Start()
    {
        randomNumber = UnityEngine.Random.Range(0, MinuteMargin);
        startMinutes = randomNumber;
        currentTime = startMinutes * 60;
        InitialSecs = currentTime * 1;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;

            if (currentTime <= 0)
            {
                timerActive = false;
                Start();
                StopTimer();
            }

            if (Mathf.Round(currentTime) == 0)
            {
                Data.Name = Manager.CurrentPlayerChoice?.PlayerName;
                MenuSwitcher.EndGame();
            }


        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        currentTimeText.text = time.ToString(@"mm\:ss");

        TimerImg.fillAmount -= 1.0f / InitialSecs * Time.deltaTime;

    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}