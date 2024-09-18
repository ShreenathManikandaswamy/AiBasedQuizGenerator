using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class QuizGenerator : MonoBehaviour
{
    [SerializeField]
    private Keys keys;
    [SerializeField]
    private TMP_InputField topic;
    [SerializeField]
    private TMP_InputField numberOfQuestions;
    [SerializeField]
    private TMP_Dropdown difficulty;
    [SerializeField]
    private TMP_Dropdown language;

    private string difficultyValue;
    private string languageValue;
    private string url;
    private List<ResponseData> responseData;

    private void Awake()
    {
        topic.text = "Fruits and vegetables";
        numberOfQuestions.text = "5";
        difficultyValue = difficulty.options[0].text;
        languageValue = language.options[0].text;
        url = keys.googleVertextUrl + "?topic=" + ReplaceWhitespace(topic.text) + "&num_q=" + ReplaceWhitespace(numberOfQuestions.text)
            + "&diff=" + ReplaceWhitespace(difficultyValue) + "&lang=" + languageValue;
    }

    public void Generate()
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseValue = webRequest.downloadHandler.text;
                    responseData = JsonConvert.DeserializeObject<List<ResponseData>>(responseValue);
                    break;
            }
        }
    }

    private string ReplaceWhitespace(string input)
    {
        return input.Replace(" ", "");
    }
}
