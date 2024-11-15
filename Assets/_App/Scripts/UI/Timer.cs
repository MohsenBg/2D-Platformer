using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{

    private UIDocument uiDocument;
    private Label timerLabel;

    [HideInInspector] public TimeSpan time;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        timerLabel = root.Query<Label>("TimerLabel").First();
    }

    void Start()
    {
        time = TimeSpan.FromSeconds(0);
    }


    void Update()
    {
        time += TimeSpan.FromSeconds(Time.deltaTime);
        timerLabel.text = TimerToString();
    }


    public string TimerToString()
    {
        string minutes = time.Minutes > 0 ? time.Minutes.ToString() + ":" : "";
        string seconds = time.Seconds.ToString("D2");
        string milliseconds = time.Milliseconds.ToString()[0].ToString();
        return timerLabel.text = minutes + seconds + ":" + milliseconds;
    }

    public void ResetTimer()
    {
        time = TimeSpan.FromSeconds(Time.deltaTime);
    }
}
