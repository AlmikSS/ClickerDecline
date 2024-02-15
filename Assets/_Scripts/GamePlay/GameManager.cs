using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _lostPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _pausePanel;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timerText;
    
    [Header("Propertions")]
    [SerializeField] private float _levelTime;
    [SerializeField] private int _elementCountForWin;

    [SerializeField] private ElementSpawner _spawner;
    
    private Timer _timer;
    private int _score;
    private bool _isPaused = false;
    public static bool IsPlaying = true;

    private void Start()
    {
        _scoreText.text = $"Score: {_score}";
        Player.ElementDestroyed += AddSore;
        Player.PauseButtonPressed += Pause;
        _timer = new Timer(this);
        _timer.Set(_levelTime);
        _timer.HasBeenUpdate += UpdateTimer;
        _timer.TimeIsOver += Lose;
        _timer.StartCountingTime();
    }

    private void UpdateTimer(float remineTime)
    {
        _timerText.text = $"Time: {Mathf.RoundToInt(remineTime)}";
    }
    
    private void AddSore()
    {
        if (IsPlaying)
            _score++;
        _scoreText.text = $"Score: {_score}";

        if (_score == _elementCountForWin)
            Win();
    }

    private void Pause()
    {
        if (!_isPaused)
        {
            _isPaused = true;
            IsPlaying = false;
            _pausePanel.SetActive(true);
            _timer.StopCountingTime();
            _spawner.StopAllCoroutines();
        }
        else if (_isPaused)
        {
            _isPaused = false;
            IsPlaying = true;
            _pausePanel.SetActive(false);
            _timer.StartCountingTime();
            _spawner.StartCoroutine(_spawner.SpawnElements());
        }
    }
    
    private void Win()
    {
        IsPlaying = false;
        _winPanel.SetActive(true);
        _timer.StopCountingTime();
        _spawner.StopAllCoroutines();
    }
    
    private void Lose()
    {
        IsPlaying = false;
        _lostPanel.SetActive(true);
        _timer.StopCountingTime();
        _spawner.StopAllCoroutines();
    }
}