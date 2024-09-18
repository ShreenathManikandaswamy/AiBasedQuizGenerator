using TMPro;
using UnityEngine;

public class ReportHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private QuizManager manager;

    public void ShowReport(int score, int totalQuestions, QuizManager quizManager)
    {
        manager = quizManager;
        scoreText.text = "Final Score = " + score + " / " + totalQuestions;
    }

    public void Restart()
    {
        manager.ResetQuiz();
    }
}
