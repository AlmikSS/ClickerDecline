using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private float _time;
    private float _reminingTime;
    
    public event Action TimeIsOver;
    public event Action<float> HasBeenUpdate;

    private MonoBehaviour _context;
    private IEnumerator _countDown;
    
    public Timer(MonoBehaviour context) => _context = context;

    public void Set(float time)
    {
        _time = time;
        _reminingTime = _time;
    }

    public void StartCountingTime()
    {
        _countDown = CountDown();
        _context.StartCoroutine(_countDown);
    }

    public void StopCountingTime()
    {
        if (_countDown != null)
            _context.StopCoroutine(_countDown);
    }

    private IEnumerator CountDown()
    {
        while (_reminingTime >= 0)
        {
            _reminingTime -= Time.deltaTime;
            
            HasBeenUpdate?.Invoke(_reminingTime);
            
            yield return null;
        }
        
        TimeIsOver?.Invoke();
    }
}