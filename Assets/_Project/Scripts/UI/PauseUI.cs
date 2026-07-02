using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnPause += ShowPause;
        GameManager.OnResume += HidePause;
    }

    private void OnDisable()
    {
        GameManager.OnPause -= ShowPause;
        GameManager.OnResume -= HidePause;
    }

    void ShowPause()
    {
        panel.SetActive(true);
    }

    void HidePause()
    {
        panel.SetActive(false);
    }

    public void Pause()
    {
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BattleScene");
    }

    public void BackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
}