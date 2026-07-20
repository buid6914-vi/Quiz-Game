using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryButton : MonoBehaviour
{
    public QuestionCategory category;  
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip choose;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        
    }

    public void SelectCategory()
    {
        PlayerPrefs.SetInt("SelectedCategory", (int)category);
        SceneManager.LoadScene("BattleScene");
        audioSource.PlayOneShot(choose);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}   