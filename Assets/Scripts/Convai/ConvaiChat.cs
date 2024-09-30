using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class ConvaiChat : MonoBehaviour
{
    // URL для API
    private const string apiUrl = "https://api.convai.com/character/getResponse";

    [SerializeField] Text _textAnswer;
    [SerializeField] InputField _text;
    // Ваш API ключ
    [SerializeField] private string convaiApiKey = "<your api key>";

    // Идентификатор персонажа и идентификатор сессии
    [SerializeField] private string charID = "<your character id>";
    private string sessionID = "-1"; // -1 для новой сессии

    // Метод для отправки текстового запроса
    public void SendTextRequest(string userText)
    {
        StartCoroutine(PostRequest(userText));
    }

    private IEnumerator PostRequest(string userText)
    {
        userText = _text.text;
        // Подготавливаем данные запроса
        var formData = new WWWForm();
        formData.AddField("userText", userText);
        formData.AddField("charID", charID);
        formData.AddField("sessionID", sessionID);
        formData.AddField("voiceResponse", "False");

        // Устанавливаем заголовки
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, formData);
        request.SetRequestHeader("CONVAI-API-KEY", convaiApiKey);

        // Отправляем запрос и ждем ответа
        yield return request.SendWebRequest();

        // Обработка ответа
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            var jsonResponse = request.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<ConvaiResponse>(jsonResponse);

            // Сохраняем sessionID для последующих запросов
            sessionID = response.sessionID;

            // Выводим текстовый ответ
            Debug.Log($"Character response: {response.text}");
            _textAnswer.text = response.text;
        }
    }

    // Класс для хранения ответа от API
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
