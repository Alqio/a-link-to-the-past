using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class HiScoreSender : MonoBehaviour
{

    public InputField scoreInputField;
    public Button SendButton;

    // Start is called before the first frame update
    void Start()
    {
        SendButton.onClick.AddListener(PostScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PostScore()
    {
        float score = ScoreManager.Instance.GetScore();

        string dateString = System.DateTime.UtcNow.ToString("o");

        string postData = "{ \"score\": " + score + ", \"scorer\": \"" + scoreInputField.text.ToString() + "\", \"hash\": \"$2b$04$I1W7D9Rhh2e22M9r62K4He66seGBzC9KspPHzSOr/bYYxivDtgkF.\", \"date\": \"" + dateString + "\"}";
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
        Debug.Log("POSTIA:" + bytes);
        

        string url = "https://gmscoreboard-backend.herokuapp.com/games/61f665133feb280004afa71c/scores/";
        UnityWebRequest request = UnityWebRequest.Put(url, bytes);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        StartCoroutine(SendRequest(request));


        // UnityWebRequest request = new UnityWebRequest(url, "POST");
        // byte[] rawBody = Encoding
    }

    private IEnumerator SendRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("POST Error while sending score " + request.GetResponseHeader(""));
        }
        else
        {
            byte[] results = request.downloadHandler.data;
            Debug.Log("POST results: " + results.ToString());
            gameObject.SetActive(false);
        }

    }
}
