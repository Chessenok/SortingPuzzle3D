using UnityEngine;
using System;

public class Timer : MonoBehaviour , ILoseFlow
{

    [SerializeField] private TMPro.TextMeshProUGUI _minutes,_minutesD,_seconds;
    private float remainingTime;
    private float _toSecond;

    public event System.Action OnLoseEvent;
    private bool _lost;

    private void Start()
    {
        LevelManager.Instance.OnWinLevel += StopTimer;
    }
    public void StartTimer(float seconds)
    {
        _lost = false;
        remainingTime = seconds;
    }

    public void StopTimer()
    {
        _lost = true;
    }

    public void Update()
    {
        if (_lost != true)
        {
            _toSecond += Time.deltaTime;
            if (_toSecond >= 1)
            {
                remainingTime -= 1f;
                _toSecond -= 1f;
                UpdateUITime();
            }
            if (remainingTime <= 0f)
            {
                OnLose();
            }
        }
    }
    public void ContinueTimer()
    {

    }
    private void UpdateUITime()
    {
        _minutes.text =  (Convert.ToInt32(remainingTime)/60).ToString();
        int seconds = Convert.ToInt32(remainingTime) % 60;
        if (seconds < 10)
        {
            _seconds.text = $"0{seconds.ToString()}";
            return;
        }
        _seconds.text = seconds.ToString();
    }
    private void OnLose()
    {
        OnLoseEvent?.Invoke();
        _lost = true;
    }
}
