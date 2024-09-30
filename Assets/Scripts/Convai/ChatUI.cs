using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public ConvaiChat convaiChat;
    public InputField inputField;

    public void OnSendButtonClicked()
    {
        string userText = inputField.text;
        convaiChat.SendTextRequest(userText);
    }
}
