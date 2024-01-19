using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timerText;

    private static GameManager _instance;
    private int score;

    private void Start()
    {
        _instance = this;
    }

    public static void AddSore()
    {
        
    }
}