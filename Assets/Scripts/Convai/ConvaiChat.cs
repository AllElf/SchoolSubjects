using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ConvaiChat : MonoBehaviour
{
    // URL ��� API
    private const string apiUrl = "https://api.convai.com/character/getResponse";

    [SerializeField] Text _textAnswer;
    [SerializeField] InputField _text;
    // ��� API ����
    [SerializeField] private string convaiApiKey = "<your api key>";

    // ������������� ��������� � ������������� ������
    [SerializeField] private string charID = "<your character id>";
    private string sessionID = "-1"; // -1 ��� ����� ������

    // ����� ��� �������� ���������� �������
    public void SendTextRequest(string userText)
    {
        StartCoroutine(PostRequest(userText));
    }

    private IEnumerator PostRequest(string userText)
    {
        userText = _text.text;
        // �������������� ������ �������
        var formData = new WWWForm();
        formData.AddField("userText", userText);
        formData.AddField("charID", charID);
        formData.AddField("sessionID", sessionID);
        formData.AddField("voiceResponse", "False");

        // ������������� ���������
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, formData);
        request.SetRequestHeader("CONVAI-API-KEY", convaiApiKey);

        // ���������� ������ � ���� ������
        yield return request.SendWebRequest();

        // ��������� ������
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            var jsonResponse = request.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<ConvaiResponse>(jsonResponse);

            // ��������� sessionID ��� ����������� ��������
            sessionID = response.sessionID;

            // ������� ��������� �����
            Debug.Log($"Character response: {response.text}");
            _textAnswer.text = response.text;
        }
    }

    // ����� ��� �������� ������ �� API
    [Serializable]
    public class ConvaiResponse
    {
        public string charID;
        public string text;
        public string audio;
        public string sample_rate;
        public string sessionID;
    }
}
