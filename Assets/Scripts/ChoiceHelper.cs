using TMPro;
using UnityEngine;

public class ChoiceHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI choice;

    private QuizHelper quizHelper;
    public string CorrectAnswerValue = "";

    public void SetupChoice(string response, QuizHelper manager)
    {
        quizHelper = manager;
        choice.text = response;
    }

    public void Selected()
    {
        if (choice.text == CorrectAnswerValue)
            quizHelper.ShowPopup(true);
        else
            quizHelper.ShowPopup(false);

    }
}
