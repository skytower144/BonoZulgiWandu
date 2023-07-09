using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private TextMeshProUGUI timerUI;
    private float timer;
    private bool enableTimer = false;

    void Start()
    {
        timer = maxTime;
        timerUI.text = timer.ToString("F0");
    }

    void Update()
    {
        if (enableTimer)
            CountDown();
    }

    private void CountDown()
    {
        int beforeTime = Mathf.FloorToInt(timer);
        timer -= Time.deltaTime;

        if (timer < 0)
            TimeOut();
        
        int afterTime = Mathf.FloorToInt(timer);
        timerUI.text = Mathf.FloorToInt(timer).ToString();
        if (timer < 4)
            timerUI.color =  Color.red;
        
        if (afterTime != beforeTime) {
            DOTween.Rewind("TimerBounce");
            DOTween.Play("TimerBounce");
        }
    }

    private void TimeOut()
    {
        timer = 0;
        timerUI.text = timer.ToString("F0");
        enableTimer = false;
        GameManager.instance.GameOver();
    }

    public void StopTimer()
    {
        enableTimer = false;
    }

    public void StartTimer()
    {
        timer = maxTime;
        enableTimer = true;
        timerUI.color = Color.black;
    }

}
