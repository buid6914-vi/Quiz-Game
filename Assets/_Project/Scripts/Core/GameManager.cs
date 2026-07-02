using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action OnGameOver;

    public GameState CurrentState { get; private set; }


    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.MainMenu:
                Debug.Log("Main Menu");
                break;

            case GameState.Playing:
                Debug.Log("Playing");
                break;

            case GameState.Paused:
                Debug.Log("Paused");
                break;

            case GameState.GameOver:
                Debug.Log("Game Over");
                break;
        }
    }
    public void ResetScore()
    {
        Score = 0;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }


    public void SaveHighScore(QuestionCategory category, int score)
    {
        string key = $"HighScore_{category}";

        int currentHigh = PlayerPrefs.GetInt(key, 0);

        if (score > currentHigh)
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();

            Debug.Log($"New HighScore [{category}] : {score}");
        }
    }

    public int GetHighScore(QuestionCategory category)
    {
        return PlayerPrefs.GetInt($"HighScore_{category}", 0);
    }
    public void GameOver()
    {  ChangeState(GameState.GameOver);
      
        OnGameOver?.Invoke();
    }
}