using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractMap : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Text _textAnswer;
    [SerializeField] Text _textResult;
    [SerializeField] GameObject _panelResult;
    [SerializeField] float _count;


    public Image _image;
    [SerializeField] float _seconds;
    public float fillAmounT = 1;
    [SerializeField] float _speed = 0.1f;
    void Start()
    {
        _seconds = _speed / _seconds;
        StartCoroutine(Clock());

        _panelResult.SetActive(false);
        _count = 0f;
    }

    private void Update()
    {
        if (_text != null)
        {
            _text.text = "Баллы: " + _count.ToString();
            _textResult.text = "Результат:\n" + "Баллы: " + _count.ToString();
        }
        KeyButton();
        
    }
    
    public void CountPlus()
    {
        if (fillAmounT > 0f)
        {
            _count = _count + 0.5f;
            _textAnswer.text = "Верно";
        }
    }
    public void CountUnchanged()
    {
        if (fillAmounT > 0f)
        {
            _textAnswer.text = "Не верно";
        }
            
    }
    public void CountMinus()
    {
        if(fillAmounT > 0f)
        {
            if (_count > 0)
            {
                _count = _count - 0.5f;
            }
            else if (_count < 0)
            {
                _count = 0;
            }
        }
        
    }
    void KeyButton()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            CountPlus();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            CountUnchanged();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            CountMinus();
        }
        else if(Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene("GeographyWorldMap");
        }
    }
    IEnumerator Clock()
    {
        while (true)
        {
            fillAmounT -= _seconds;
            yield return new WaitForSeconds(_speed);
            _image.fillAmount = fillAmounT;
            if (fillAmounT <= 0)
            {
                //fillAmounT = 1;
                _panelResult.SetActive(true);
                break;
            }
        }
    }
}
