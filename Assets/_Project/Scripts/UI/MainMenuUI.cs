using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject highScorePanel;

    [Header("Texts")]
    [SerializeField] private TMP_Text[] highScoreTexts;

    void Start()
    {
        highScorePanel.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("CategoryScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenHighScore()
    {
        highScorePanel.SetActive(true);

        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            QuestionCategory category = (QuestionCategory)i;

            int score =
                PlayerPrefs.GetInt($"HighScore_{category}", 0);

            highScoreTexts[i].text =
                $"{category} : {score}";
        }
    }

    public void CloseHighScore()
    {
        highScorePanel.SetActive(false);
    }
}