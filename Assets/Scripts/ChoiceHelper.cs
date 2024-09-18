using TMPro;
using UnityEngine;

public class ChoiceHelper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI choice;

    public void SetupChoice(string response)
    {
        choice.text = response;
    }
}
