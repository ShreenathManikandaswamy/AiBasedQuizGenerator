using UnityEngine;
using TMPro;

public class QuizHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI question;
    [SerializeField]
    private ChoiceHelper choices;
    [SerializeField]
    private Transform choiceParent;
    [SerializeField]
    private AnswerHelper answerHelperPrefab;
    [SerializeField]
    private Transform answerPopupParent;
    [SerializeField]
    private GameObject nextButton;

    private QuizManager manager;
    private ResponseData Response;
    private bool correctAnswer;

    public void SetupQuestion(ResponseData quiz, QuizManager quizManager)
    {
        manager = quizManager;
        Response = quiz;
        question.text = quiz.question;

        foreach(string response in quiz.responses)
        {
            ChoiceHelper responseInstance = Instantiate(choices, choiceParent);
            responseInstance.SetupChoice(response, this);
            responseInstance.CorrectAnswerValue = quiz.correct;
        }
    }

    public void NextQuestion()
    {
        nextButton.SetActive(false);
        manager.NextQuestion(correctAnswer);
    }

    public void ShowPopup(bool isCorrect)
    {
        correctAnswer = isCorrect;
        AnswerHelper answerInstance = Instantiate(answerHelperPrefab, answerPopupParent);
        answerInstance.explanationValue = Response.explanation;

        if (isCorrect)
            answerInstance.CorrectAnswer();
        else
            answerInstance.WrongAnswer();

        nextButton.SetActive(true);
    }
}
