using System.Collections;
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

    private List<ResponseData> quiz;
    private int count = 0;

    private void Awake()
    {
        inputScreen.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void Load()
    {
        loadingScreen.SetActive(true);
    }

    public void LoadQuestions(List<ResponseData> response)
    {
        inputScreen.SetActive(false);
        loadingScreen.SetActive(false);
        quiz = response;
        QuizHelper quizInstance = Instantiate(quizPrefab, quizParent);
        quizInstance.SetupQuestion(quiz[count]);
    }
}
