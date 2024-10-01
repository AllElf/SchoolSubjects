using UnityEngine;
using UnityEngine.UI;

public class ReadingTheText : MonoBehaviour
{
    [SerializeField] InputField _thisInputField;
    [SerializeField] string _nameText;
    public string _fullText;

    private void Start()
    {
        _thisInputField = GetComponent<InputField>();
        _nameText = _thisInputField.name;
    }
    private void FixedUpdate()
    {
        if (_thisInputField != null)
        {
            _fullText = _nameText + " " + _thisInputField.text;
        }
    }
}
