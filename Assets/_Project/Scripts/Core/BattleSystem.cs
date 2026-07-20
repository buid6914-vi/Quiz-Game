using System.Collections;
using TMPro;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance;

    [Header("Characters")]
    public CharacterBase player;
    public CharacterBase enemy;

    [Header("Question Timer")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timePerQuestion = 10f;

    private float currentTime;
    private BattleState state;
    private QuestionCategory selectedCategory;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wronganswerSource;
    
    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
        state = BattleState.Start;

        selectedCategory =
            (QuestionCategory)PlayerPrefs.GetInt("SelectedCategory", 0);

        QuestionManager.Instance.StartMatch(selectedCategory);

        QuestionManager.Instance.OnAnswerResult += HandleAnswer;

        StartCoroutine(SetupBattle());
    }

    private void OnDestroy()
    {
        if (QuestionManager.Instance != null)
            QuestionManager.Instance.OnAnswerResult -= HandleAnswer;
    }

    IEnumerator SetupBattle()
    {
        yield return new WaitForSeconds(1f);

        AskQuestion();
    }

    private void Update()
    {
        if (state != BattleState.PlayerTurn)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
            currentTime = 0;
        
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
        
        if (currentTime <= 0)
        {
            TimeUp();
        }
    }

    void TimeUp()
    {
        if (state != BattleState.PlayerTurn)
            return;

        HandleAnswer(false);
    }

    void HandleAnswer(bool correct)
    {
        
        if(GameManager.Instance.CurrentState == GameState.Paused)
            return;
        if (state != BattleState.PlayerTurn)
            return;

        ChangeState(BattleState.Busy);

        if (correct)
            StartCoroutine(PlayerAttackRoutine());
        else
            audioSource.PlayOneShot(wronganswerSource);
            StartCoroutine(EnemyAttackRoutine());
            
    }

    IEnumerator PlayerAttackRoutine()
    {
        yield return new WaitForSeconds(0.4f);

        player.Attack(enemy);

        yield return new WaitForSeconds(1f);

        if (enemy.CurrentHP <= 0)
        {
            Debug.Log("Enemy Dead");


            player.Heal(30);


            enemy.ResetHP();

            yield return new WaitForSeconds(1f);
        }

        AskQuestion();
    }

    IEnumerator EnemyAttackRoutine()
    {
        yield return new WaitForSeconds(0.4f);

        enemy.Attack(player);

        yield return new WaitForSeconds(1f);

        if (player.CurrentHP <= 0)
        {
            ChangeState(BattleState.Lost);

            GameManager.Instance.SaveHighScore(
                selectedCategory,
                QuestionManager.Instance.Score);

            GameManager.Instance.GameOver();

            yield break;
        }

        AskQuestion();
    }

    public void AskQuestion()
    {
        currentTime = timePerQuestion;

        ChangeState(BattleState.PlayerTurn);

        QuestionManager.Instance.ShowNextQuestion();
    }

    public void ChangeState(BattleState newState)
    {
        state = newState;
    }
}