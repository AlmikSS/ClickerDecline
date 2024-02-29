using System.Collections;
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
    [SerializeField] private TMP_Text _levelTimerText;
    [SerializeField] private TMP_Text _startTimerText;
    
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
        _levelTimerText.text = $"Time: {Mathf.RoundToInt(_levelTime)}";
        _scoreText.text = $"Score: {_score}";

        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        for (int i = 0; i < 3; i++)
        {
            _startTimerText.text = (i + 1).ToString();
            yield return new WaitForSeconds(1);
        }

        _startTimerText.text = "Жги!";
        _scoreText.gameObject.GetComponent<Animator>().Play("GoToIdle");
        _levelTimerText.gameObject.GetComponent<Animator>().Play("GoToIdle");
        yield return new WaitForSeconds(1);
        _startTimerText.gameObject.SetActive(false);
        StartTimer();
        StartCoroutine(_spawner.SpawnElements());
    }
    
    private void StartTimer()
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
        _levelTimerText.text = $"Time: {Mathf.RoundToInt(remineTime)}";
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
            StartCoroutine(_spawner.SpawnElements());
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