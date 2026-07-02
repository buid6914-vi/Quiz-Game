using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryButton : MonoBehaviour
{
    public QuestionCategory category;  

    public void SelectCategory()
    {
        PlayerPrefs.SetInt("SelectedCategory", (int)category);
        SceneManager.LoadScene("BattleScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}   