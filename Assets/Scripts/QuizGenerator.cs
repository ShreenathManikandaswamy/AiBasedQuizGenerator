using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class QuizGenerator : MonoBehaviour
{
    [SerializeField]
    private Keys keys;

    private List<ResponseData> responseData;

    private void Start()
    {
        StartCoroutine(GetRequest(keys.googleVertextUrl));
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
                    string responseValue = ProcessResponse(webRequest.downloadHandler.text);
                    Debug.Log(responseValue);
                    responseData = JsonConvert.DeserializeObject<List<ResponseData>>(responseValue);
                    Debug.Log(responseData.Count);
                    break;
            }
        }
    }

    private string ProcessResponse(string response)
    {
        string[] output = response.Split("json\n");
        string[] value = output[1].Split("\n`");
        return value[0];
    }
}
