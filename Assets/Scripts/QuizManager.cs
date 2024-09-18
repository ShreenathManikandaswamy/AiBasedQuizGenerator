using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inputScreen;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private QuizHelper quizPrefab;
    [SerializeField]
    private Transform quizParent;
    [SerializeField]
    private TextMeshProUGUI scoreValue;
    [SerializeField]
    private ReportHelper reportHelperPrefab;

    private List<ResponseData> quiz;
    private QuizHelper quizInstance;
    private ReportHelper reportInstance;
    private int count = 0;
    private int score = 0;

    private void Awake()
    {
        inputScreen.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void Load()
    {
        loadingScreen.SetActive(true);
    }

    public void NextQuestion(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
        {
            score++;
            scoreValue.text = "Score = " + score + " / " + quiz.Count;
        }

        if (count < quiz.Count - 1)
        {
            count++;
            if (quizInstance != null)
                Destroy(quizInstance.gameObject);

            LoadQuizComponent();
        }else
        {
            reportInstance = Instantiate(reportHelperPrefab, quizParent);
            reportInstance.ShowReport(score, quiz.Count, this);
        }
    }

    public void ResetQuiz()
    {
        count = 0;
        score = 0;
        scoreValue.text = "Score = " + score + " / " + quiz.Count;

        if (reportInstance != null)
            Destroy(reportInstance.gameObject);

        if (quizInstance != null)
            Destroy(quizInstance.gameObject);

        inputScreen.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void LoadQuestions(List<ResponseData> response)
    {
        inputScreen.SetActive(false);
        loadingScreen.SetActive(false);
        quiz = response;
        LoadQuizComponent();
    }

    private void LoadQuizComponent()
    {
        quizInstance = Instantiate(quizPrefab, quizParent);
        quizInstance.SetupQuestion(quiz[count], this);
    }
}
