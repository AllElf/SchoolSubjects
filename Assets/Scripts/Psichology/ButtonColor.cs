using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    [SerializeField] Color _color = Color.green;
    [SerializeField] Button _button;

    private void Start()
    {  
        if (GetComponent<Button>() != null) 
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ChangingTheColor);
        }
    }

    public void ChangingTheColor()
    {
        if (GetComponent<Image>().color != null)
        {
            GetComponent<Image>().color = _color;
        } 
    }
}
