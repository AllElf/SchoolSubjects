using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestionsForParents5thGrade : MonoBehaviour
{
    [Header("Ответы")]
    public string _answer4;
    public string _answer5;
    public string _answer15;
    public string _answer16;
    public string[] _answer6;
    [Header("Все ответы в одном")]
    [Multiline]
    public string _fullAnswer;
    [Header("ReadingTheText скрипты")]
    ReadingTheText[] _readingTheText;
    [Header("Кнопки для 4 вопроса")]
    [SerializeField] Button[] buttons4;
    [Header("inputField для 4,6,15вопроссов")]
    [SerializeField] InputField inputField4;
    [SerializeField] InputField inputField15;
    [SerializeField] InputField inputField16;
    [Header("Поиск основного скрипта")]
    [SerializeField]CountingInformationPsychology _countingInformationPsychology;
    public bool _enabled;
    //[Header("RaycastUI скрипт")]
    //[SerializeField] RaycastUI raycastUI;

    private void Awake()
    {
        _enabled = false;
        _answer6 = new string[GameObject.FindGameObjectsWithTag("Answer6").Length];
        _readingTheText = GameObject.FindObjectsOfType<ReadingTheText>();
    }
    private void Start()
    {
        
        
    }
    private void FixedUpdate()
    {
        InputField4Editing();
        Answer6_1();
        FullAnswer();
    }
    public void EnabledPanel()
    {
        _enabled = true;
    }
    public void FullAnswer()
    {
        if(_enabled == true)
        {
            _fullAnswer = "Ответы за 4 вопрос: \n" + _answer4 + "\n" + "Ответы за 5 вопрос: \n" + _answer5 + "\n" + "Ответы за 6 вопрос \n: "
            + _answer6[0] + "\n" + _answer6[1] + "\n" + _answer6[2] + "\n" + _answer6[3] + "\n" + _answer6[4] + "\n" + _answer6[5]
            + "\n" + _answer6[6] + "\n" + _answer6[7] + "\n" + _answer6[8] + "\n" + _answer6[9] + "\n" + _answer6[10] + "\n" + _answer6[11]
            + "\n" + _answer6[12] + "\n" + "Ответы за 15 вопрос \n: " + _answer15 + "\n" + "Ответы за 16 вопрос \n: " + _answer16 + "\n";
        }
        else if (_enabled == false)
        {
            _fullAnswer = "Пусто";
      
        }
    }
    public void InputField4Click()
    {
       string text = "4.1 - " + inputField4.text;
        inputField4.text = text;
    }
    public void InputField4Editing()
    {
        if (inputField4.text == "" && _answer4 == "")
        {
            
            for (int i = 0; i < buttons4.Length; i++)
            {
                buttons4[i].interactable = true;
            }
        }
        if (inputField4.text != "")
        {
            _answer4 = inputField4.text;
            for (int i = 0; i < buttons4.Length; i++)
            {
                buttons4[i].interactable = false;
            }
        }
        if (inputField4.text == "")
        {
            _answer4 = "";
            for (int i = 0; i < buttons4.Length; i++)
            {
                buttons4[i].interactable = true;
            }
        }
    }
    public void Answer4_1()
    {
        _answer4 = "4.1 - зазыпает с трудом";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_2()
    {
        _answer4 = "4.1 - долго не может заснуть, хотя очень устал";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_3()
    {
        _answer4 = "4.1 - внезапно просыпается ночью, плачет";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_4()
    {
        _answer4 = "4.1 - разговаривает во сне";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_5()
    {
        _answer4 = "4.1 - просыпается с трудом";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_6()
    {
        _answer4 = "4.1 - утром сонный и вялый";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_7()
    {
        _answer4 = "4.1 - недержание мочи";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_8()
    {
        _answer4 = "4.1 - плохой аппетит";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_9()
    {
        _answer4 = "4.1 - после школы вялый,  уставший, раздражительный, перевозбуждённый";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_10()
    {
        _answer4 = "4.1 - беспричинные боли в животе";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_11()
    {
        _answer4 = "4.1 ― частые головные боли";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_12()
    {
        _answer4 = "4.1 ― болел в сентябре-октябре";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_13()
    {
        _answer4 = "4.1 ― стал сосать пальцы, грызть ногти, кусать губы, ковыряться в носу, теребить волосы или многократно повторять какие-либо действия";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_14()
    {
        _answer4 = "4.1 ― наблюдаются быстрые подёргивания (тики) лицевых мышц, плеч, рук и т.п";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer4_15()
    {
        _answer4 = "4.1 ― ведёт себя как маленький, не соответственно возрасту";
        inputField4.text = _answer4;
        inputField4.interactable = false;
        for (int i = 0; i < buttons4.Length; i++)
        {
            buttons4[i].interactable = false;
        }
    }
    public void Answer5_1()
    {
        _answer5 = "5.1 да";
    }
    public void Answer5_2()
    {
        _answer5 = "5.2 нет";
    }
    public void Answer6_1()
    {
        for (int i = 0; i < _readingTheText.Length; i++)
        {
            _answer6[i] = _readingTheText[i]._fullText;
        }
    }
    
    public void Answer15()
    {
        _answer15 = "15. " + inputField15.text;
    }
    public void Answer16()
    {
        _answer16 = "16. " + inputField16.text;
    }
}
