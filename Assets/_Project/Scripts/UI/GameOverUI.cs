using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        
        GameManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ShowGameOver;
    }

    void ShowGameOver()
    {
        panel.SetActive(true);
        scoreText.text =
            "Score : " + GameManager.Instance.Score;

        QuestionCategory category =
            (QuestionCategory)PlayerPrefs.GetInt("SelectedCategory", 0);
        int highScore =
            GameManager.Instance.GetHighScore(category);

        highScoreText.text =
            "High Score : " + highScore;
    }

    //====================================================

    public void Retry()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}