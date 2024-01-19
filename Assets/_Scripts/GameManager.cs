using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _levelTime;
    [SerializeField] private int _elementCountForWin;
    
    private static GameManager _instance;
    private Timer _timer;
    private int _score;

    private void Start()
    {
        _instance = this;
        _timer = new Timer(this);
        _timer.Set(_levelTime);
        _timer.HasBeenUpdate += UpdateTimer;
        _timer.TimeIsOver += Lose;
        _timer.StartCountingTime();
    }

    private void Lose()
    {
        Debug.Log("Los");
    }
    
    private void UpdateTimer(float remineTime)
    {
        _timerText.text = $"Time: {Mathf.RoundToInt(remineTime)}";
    }
    
    private void AddSore()
    {
        _instance._score++;
        _instance._scoreText.text = $"Score: {_instance._score}";
    }
}