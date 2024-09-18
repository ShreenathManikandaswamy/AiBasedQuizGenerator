using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class AnswerHelper : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteValues;
    [SerializeField]
    private string[] responseString;
    [SerializeField]
    private Image correctnessImage;
    [SerializeField]
    private TextMeshProUGUI correctnessText;
    [SerializeField]
    private TextMeshProUGUI explanation;

    public string explanationValue;

    public void CorrectAnswer()
    {
        correctnessImage.sprite = spriteValues[0];
        correctnessText.text = responseString[0];
        explanation.text = explanationValue;
    }

    public void WrongAnswer()
    {
        correctnessImage.sprite = spriteValues[1];
        correctnessText.text = responseString[1];
        explanation.text = explanationValue;
    }
}
