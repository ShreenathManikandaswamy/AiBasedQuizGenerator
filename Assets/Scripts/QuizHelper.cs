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

    public void SetupQuestion(ResponseData quiz)
    {
        question.text = quiz.question;

        foreach(string response in quiz.responses)
        {
            ChoiceHelper responseInstance = Instantiate(choices, choiceParent);
            responseInstance.SetupChoice(response);
        }
    }
}
