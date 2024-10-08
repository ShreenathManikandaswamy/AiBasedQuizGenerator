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
    private QuizManager manager;
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
    private string topicValue;
    private string questionsCount;
    private string url;
    private List<ResponseData> responseData;
    private int diffCount = 0;
    private int langCount = 0;

    private void Awake()
    {
        topic.text = "Fruits and vegetables";
        numberOfQuestions.text = "5";
        difficultyValue = difficulty.options[diffCount].text;
        languageValue = language.options[langCount].text;
        url = keys.googleVertextUrl + "?topic=" + ReplaceWhitespace(topic.text) + "&num_q=" + ReplaceWhitespace(numberOfQuestions.text)
            + "&diff=" + ReplaceWhitespace(difficultyValue) + "&lang=" + languageValue;
    }

    public void OnDiffChange(int value)
    {
        diffCount = value;
    }

    public void OnLangChange(int value)
    {
        langCount = value;
    }

    public void Generate()
    {
        manager.Load();
        topicValue = topic.text;
        questionsCount = numberOfQuestions.text;
        difficultyValue = difficulty.options[diffCount].text;
        languageValue = language.options[langCount].text;
        url = keys.googleVertextUrl + "?topic=" + ReplaceWhitespace(topicValue) + "&num_q=" + ReplaceWhitespace(questionsCount)
            + "&diff=" + ReplaceWhitespace(difficultyValue) + "&lang=" + languageValue;
        Debug.Log(url);
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
                    Debug.Log(responseValue);
                    if(responseValue.Contains("json"))
                    {
                        responseValue = FixJson(responseValue);
                    }
                    responseData = JsonConvert.DeserializeObject<List<ResponseData>>(responseValue);
                    manager.LoadQuestions(responseData);
                    break;
            }
        }
    }

    private string FixJson(string json)
    {
        string[] jsonRemove = json.Split("json");
        string[] output = jsonRemove[1].Split("```");
        return output[0];
    }

    private string ReplaceWhitespace(string input)
    {
        return input.Replace(" ", "");
    }
}
