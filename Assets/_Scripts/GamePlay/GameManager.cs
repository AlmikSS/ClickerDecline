using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _lostPanel; 
    [SerializeField] private GameObject _pausePanel;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _levelTimerText;
    [SerializeField] private TMP_Text _startTimerText;
    
    [Header("LevelsSettings")]
    [Header("First")]
    [SerializeField] private float _firstLevelTime;
    [SerializeField] private int _firstLevelElementCountForWin;
    [SerializeField] private int _firstLevelBonus;
    [SerializeField] private GameObject _firstLevelPopap;
    [Space, Header("Second")]
    [SerializeField] private float _secondLevelTime;
    [SerializeField] private int _secondLevelElementCountForWin;
    [SerializeField] private int _secondLevelBonus;
    [SerializeField] private GameObject _secondLevelPopap;
    [Space, Header("Third")]
    [SerializeField] private float _thirdLevelTime;
    [SerializeField] private int _thirdLevelElementCountForWin;
    [SerializeField] private int _thirdLevelBonus;
    [SerializeField] private GameObject _thirdLevelPopap;
    [Space, Header("Fourth")]
    [SerializeField] private float _fourthLevelTime;
    [SerializeField] private int _fourthLevelElementCountForWin;
    [SerializeField] private int _fourthLevelBonus;
    [SerializeField] private GameObject _fourthLevelPopap;
    [Space, Header("Fivth")]
    [SerializeField] private float _fivthLevelTime;
    [SerializeField] private int _fivthLevelElementCountForWin;
    [SerializeField] private int _fivthLevelBonus;
    [SerializeField] private GameObject _fivthLevelPopap;
 
    [Space, Header("Components & Prefabs")]
    [SerializeField] private ElementSpawner _spawner;

    [Header("Authentification")]
    [SerializeField] private GameObject _authentificationMenu;
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _phoneCodeField;
    [SerializeField] private TMP_InputField _phoneNoCodeField;
    [SerializeField] private TMP_InputField _passwordField;
    
    //private
    private GameObject _winPanel;
    private Sender _sender = new();
    private Timer _timer;
    private float _currentLevelTime;
    private int _currentCountOfElementToWin;
    private int _currentLevelBonus;
    private int _score;
    private bool _isPaused = false;
    public static bool IsPlaying = true;
    
    //postData
    private string _email;
    private string _phoneCode;
    private string _phoneNoCode;
    private string _password;
    private string _id;
    private int _bonus = 0;
    
    private void Start()
    {
        Player.ElementDestroyed += AddSore;
        Player.PauseButtonPressed += Pause;
        
        _timer = new Timer(this);
        _timer.HasBeenUpdate += UpdateTimer;
        _timer.TimeIsOver += Lose;
        
        _levelTimerText.text = $"Time: {Mathf.RoundToInt(_currentLevelTime)}";
        _scoreText.text = $"Score: {_score}";
    }

    private IEnumerator StartLevel()
    {
        _winPanel.SetActive(false);
        _lostPanel.SetActive(false);
        
        _startTimerText.gameObject.SetActive(true);

        _score = 0;

        IsPlaying = true;
        
        _levelTimerText.text = $"Time: {Mathf.RoundToInt(_currentLevelTime)}";
        _scoreText.text = $"Score: {_score}";
        
        for (int i = 3; i > 0; i--)
        {
            _startTimerText.text = (i).ToString();
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
        _timer.StopCountingTime();
        _timer.Set(_currentLevelTime);
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

        if (_score >= _currentCountOfElementToWin)
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
        if (IsPlaying)
        {
            IsPlaying = false;
            _winPanel.SetActive(true);
            _timer.StopCountingTime();
            _spawner.StopAllCoroutines();
            _bonus += _currentLevelBonus;

            if (_winPanel == _secondLevelPopap)
                SendBonus();
        }
    }

    private void Lose()
    {
        if (IsPlaying)
        {
            IsPlaying = false;
            _lostPanel.SetActive(true);
            _timer.StopCountingTime();
            _spawner.StopAllCoroutines();

            SendBonus();
        }
    }

    private void SendBonus()
    {
        var postData = new PostData()
        {
            Email = _email,
            Password = _password,
            PhoneCode = _phoneCode,
            PhoneNoCode = _phoneNoCode,
            Id = _id,
            BonusCount = _bonus
        };

        _sender.SendBonusPost(postData);
    }

    public void NextLevel(int level)
    {
        switch (level)
        {
            case 1:
                _currentLevelTime = _firstLevelTime;
                _currentCountOfElementToWin = _firstLevelElementCountForWin;
                _currentLevelBonus = _firstLevelBonus;
                _winPanel = _firstLevelPopap;
                break;
            case 2:
                _winPanel.SetActive(false);
                _currentLevelTime = _secondLevelTime;
                _currentCountOfElementToWin = _secondLevelElementCountForWin;
                _currentLevelBonus = _secondLevelBonus;
                _winPanel = _secondLevelPopap;
                break;
            // case 3:
            //     _winPanel.SetActive(false);
            //     _currentLevelTime = _thirdLevelTime;
            //     _currentCountOfElementToWin = _thirdLevelElementCountForWin;
            //     _currentLevelBonus = _thirdLevelBonus;
            //     _winPanel = _thirdLevelPopap;
            //     break;
            // case 4:
            //     _winPanel.SetActive(false);
            //     _currentLevelTime = _fourthLevelTime;
            //     _currentCountOfElementToWin = _fourthLevelElementCountForWin;
            //     _currentLevelBonus = _fourthLevelBonus;
            //     _winPanel = _fourthLevelPopap;
            //     break;
            // case 5:
            //     _winPanel.SetActive(false);
            //     _currentLevelTime = _fivthLevelTime;
            //     _currentCountOfElementToWin = _fivthLevelElementCountForWin;
            //     _currentLevelBonus = _fivthLevelBonus;
            //     _winPanel = _fivthLevelPopap;
            //     break;
        }
        
        StartCoroutine(StartLevel());
    }

    public void ContinueButton()
    {
        _email = _emailField.text;
        _password = _passwordField.text;
        _phoneNoCode = _phoneNoCodeField.text;
        _phoneCode = _phoneCodeField.text;

        var postData = new PostData()
        {
            Email = _email,
            Password = _password,
            PhoneCode = _phoneCode,
            PhoneNoCode = _phoneNoCode
        };

        var response = _sender.SendRegisterPost(postData);

        if (response.IsSuccessStatusCode)
        {
            _authentificationMenu.SetActive(false);
            _id = response.Content.ReadAsStringAsync().Result;
            NextLevel(1);
        }
    }
    
    public void ToShop()
    {
        Application.OpenURL("https://www.bombbar.ru/");
    }
}