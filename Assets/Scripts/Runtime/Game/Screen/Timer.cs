using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public delegate void TimerEnd(EventArgs args);

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    public event TimerEnd timerEndEvent = delegate{ };
    public bool startOnAwake = true;
    public float time;
    private float currentTime;
    public bool active, showHours;

    private Text timerText;

    public void Start()
    {
        timerText = GetComponent<Text>();
    }

    public void Awake()
    {
        currentTime = time;

        if (startOnAwake)
            Play();
    }

    public void Play()
    {
        active = true;
    }

    public void Stop()
    {
        active = false;
    }

    public void Reset()
    {
        currentTime = time;
    }

    private void Update()
	{
		showHours = currentTime > 3600;

        if (active)
        {
            currentTime = currentTime <= 0 ? 0 : currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                timerEndEvent(new EventArgs());
                Stop();
            }
        }
        TimeSpan t = TimeSpan.FromSeconds(currentTime);
        string text = "";

        text = showHours ? String.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds) : String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

        timerText.text = text;
    }
}