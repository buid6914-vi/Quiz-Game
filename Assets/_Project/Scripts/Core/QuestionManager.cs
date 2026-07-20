using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    public Action<bool> OnAnswerResult;

    [SerializeField] private GameObject questionPanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;

    private List<QuestionData> allQuestions = new();
    private List<QuestionData> matchQuestions = new();

    private QuestionData currentQuestion;

    private int currentQuestionIndex;

    public int Score { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip nextqsSource;
    [SerializeField] private AudioClip correctanswerSource;
    [SerializeField] private AudioClip wronganswerSource;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource=GetComponent<AudioSource>();
        LoadQuestions();
    }
    


    void LoadQuestions()
    {
        TextAsset json = Resources.Load<TextAsset>("questions");

        if (json == null)
        {
            Debug.LogError("Không tìm thấy questions.json trong Resources");
            return;
        }

        QuestionList data = JsonUtility.FromJson<QuestionList>(json.text);

        allQuestions = data.questions;

        Debug.Log("Loaded " + allQuestions.Count + " questions");
    }


    public void StartMatch(QuestionCategory category)
    {
        Score = 0;
        scoreText.text = "0";

        List<QuestionData> filtered =
            allQuestions.FindAll(q => q.category == (int)category);

        if (filtered.Count == 0)
        {
            Debug.LogError("Không có câu hỏi!");
            return;
        }

        Shuffle(filtered);

        matchQuestions = new List<QuestionData>(filtered);

        currentQuestionIndex = 0;
    }


    public void ShowNextQuestion()
    {
        
        if (matchQuestions == null || matchQuestions.Count == 0)
            return;

        if (currentQuestionIndex >= matchQuestions.Count)
        {
            Shuffle(matchQuestions);
            currentQuestionIndex = 0;
        }

        currentQuestion = matchQuestions[currentQuestionIndex];
        currentQuestionIndex++;

        progressText.text =
            $"Question {currentQuestionIndex}";

        questionPanel.SetActive(true);
        audioSource.PlayOneShot(nextqsSource);
        questionText.text = currentQuestion.questionText;

        SetupAnswers();
    }

    

    void SetupAnswers()
    {
        List<AnswerOption> answers = new();

        for (int i = 0; i < currentQuestion.answers.Length; i++)
        {
            answers.Add(new AnswerOption()
            {
                text = currentQuestion.answers[i],
                isCorrect = i == currentQuestion.correctIndex
            });
        }

        Shuffle(answers);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button btn = answerButtons[i];

            if (i >= answers.Count)
            {
                btn.gameObject.SetActive(false);
                continue;
            }

            btn.gameObject.SetActive(true);

            btn.interactable = true;

            btn.GetComponent<Image>().color = normalColor;

            btn.GetComponentInChildren<TextMeshProUGUI>().text =
                answers[i].text;

            AnswerOption option = answers[i];
            
            btn.onClick.RemoveAllListeners();

            btn.onClick.AddListener(() =>
            {
                
                foreach (Button b in answerButtons)
                    b.interactable = false;

                if (option.isCorrect)
                {
                    btn.GetComponent<Image>().color = correctColor;
                    audioSource.PlayOneShot(correctanswerSource);
                    Score += 10;
                    GameManager.Instance.AddScore(10);
                }
                else
                {
                    
                    btn.GetComponent<Image>().color = wrongColor;
                    audioSource.PlayOneShot(wronganswerSource);
                    
                    foreach (Button b in answerButtons)
                    {
                        if (b.GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.answers[currentQuestion.correctIndex])
                        {
                            b.GetComponent<Image>().color = correctColor;
                            break;
                        }
                    }
                }

                scoreText.text = Score.ToString();

                StartCoroutine(DelayedSubmit(option.isCorrect));
            });
        }
    }

    

    IEnumerator DelayedSubmit(bool result)
    {
        yield return new WaitForSeconds(0.5f);

        questionPanel.SetActive(false);

        OnAnswerResult?.Invoke(result);
    }

    //============================================================

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(i, list.Count);

            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }

    //============================================================

    class AnswerOption
    {
        public string text;

        public bool isCorrect;
    }
}