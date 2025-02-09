using UnityEngine;
using UnityEngine.UI;

public class TypeOfTemperament : MonoBehaviour
{
    [SerializeField] TemperamentResult _temperamentResult;
    public Button myButton; // —сылка на кнопку
    [SerializeField] float _number;

    [SerializeField] string _melancholic = "Melancholic", _choleric = "Choleric", _sanguine = "Sanguine", _phlegmatic = "Phlegmatic";
    private void Start()
    {
        myButton = GetComponent<Button>();
        if (GameObject.FindObjectOfType<TemperamentResult>() != null)
        {
            _temperamentResult = GameObject.FindObjectOfType<TemperamentResult>();
        }
        AutoDefinition();
        _number = float.Parse(gameObject.name);
    }
    void AutoDefinition()
    {
        if (gameObject.tag == _melancholic)
        {
            myButton.onClick.AddListener(Melancholic);
        }
        else if (gameObject.tag == _choleric)
        {
            myButton.onClick.AddListener(Choleric);
        }
        else if (gameObject.tag == _sanguine)
        {
            myButton.onClick.AddListener(Sanguine);
        }
        else if (gameObject.tag == _phlegmatic)
        {
            myButton.onClick.AddListener(Phlegmatic);
        }
    }
    public void Melancholic()
    {
        _temperamentResult._melancholic += _number; 
    }
    public void Choleric()
    {
        _temperamentResult._choleric += _number;
    }
    public void Sanguine()
    {
        _temperamentResult._sanguine += _number;
    }
    public void Phlegmatic()
    {
        _temperamentResult._phlegmatic += _number;
    }

}
