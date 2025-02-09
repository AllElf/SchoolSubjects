using UnityEngine;
using UnityEngine.UI;

public class ChatRequest : MonoBehaviour
{
    [SerializeField] InputField _inputField;
    [SerializeField] ConvaiChat _convaiChat;
    [SerializeField] Image _image;
    [SerializeField] Color _color1;
    [SerializeField] Color _color2;
    [SerializeField] KeyCode _key;

    private void Update()
    {
        if (_inputField.text != "")
        {
            if (Input.GetKeyDown(_key))
            {
                _image.color = _color1;
            }
            else if (Input.GetKeyUp(_key))
            {
                _image.color = _color2;
                if(_convaiChat != null)
                {
                    ClearText();
                }
                
            }
        }
        
    }
    public void ClearText()
    {
        _convaiChat.SendTextRequest(_inputField.text);
        _inputField.text = "";
    }
}
