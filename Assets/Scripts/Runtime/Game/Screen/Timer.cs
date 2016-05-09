using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/// <summary>
/// Timer end event delegate.
/// </summary>
public delegate void TimerEnd(EventArgs args);

/// <summary>
/// Timer.
/// </summary>
[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
	/// <summary>
	/// Occurs when the timer ends.
	/// </summary>
    public event TimerEnd timerEndEvent = delegate{ };
	/// <summary>
	/// Start on wakeup?
	/// </summary>
    public bool startOnAwake = true;
	/// <summary>
	/// The time.
	/// </summary>
    public float time;
	/// <summary>
	/// The current time.
	/// </summary>
    private float currentTime;
    public bool active, showHours;

	/// <summary>
	/// The timer text.
	/// </summary>
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

	/// <summary>
	/// Play this instance.
	/// </summary>
    public void Play()
    {
        active = true;
    }

	/// <summary>
	/// Stop this instance.
	/// </summary>
    public void Stop()
    {
        active = false;
    }

	/// <summary>
	/// Reset this instance.
	/// </summary>
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