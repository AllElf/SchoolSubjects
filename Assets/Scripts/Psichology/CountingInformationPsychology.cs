using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CountingInformationPsychology : MonoBehaviour
{
    [Header("����� ���������� ��� ����� ��������/�������")]
    [SerializeField] InputField _nameText; //��������� ���� ��� �����
    [SerializeField] InputField _nameTextChildren;// ��������� ���� ��� ����� ������
    [Header("������ ��� ���������")]
    [SerializeField] GameObject[] _allObjects;
    [SerializeField] GameObject[] _results; // ��������� ���� � ������������;
    [Header("���")]
    [SerializeField] string _name; // ������� � ���
    [Header("��� �������")]
    [SerializeField] string _nameChildren; // ������� � ��� ������
    [Header("�����")]
    [SerializeField] string _class; // �����
    [Header("��������� ������")]
    [SerializeField] string _classCategory; // ��������� ������
    [Header("�������")]
    [SerializeField] string _subject; // ������� � ����������
    [Header("��������� �����")]
    [SerializeField] string _answer; // �����
    [Header("����� ����������")]
    [Multiline][SerializeField] string _allInformation; // ����� ����������
    [Header("��������/������")]
    [SerializeField] bool _childrenActivization;
    [Header("������� ��������� �����")]
    [SerializeField] bool _answerText;
    [Header("�����")]
    [SerializeField] int _scores = 0; // �����

    [Header("����� ��� ������ � �����������")]
    [SerializeField] string _url;
    bool succesful = true;

    [Obsolete]
    public void Http()
    {
        StartCoroutine(sendTextToFile());
    }
    [Obsolete]
    IEnumerator sendTextToFile()
    {
        var Date = DateTime.Now;
        succesful = true;
        string s = _allInformation + Date.ToShortDateString() + "\n";
        WWWForm form = new WWWForm();
        form.AddField("name", s);
        WWW www = new WWW(_url, form);

        yield return www;
        if (www.error != null)
        {
            succesful = false;
        }
        else
        {
            Debug.Log(www.text);
            succesful = true;
        }
    }
    public void URL()
    {
        // 5 ������

        //5� ������ ������
        if (_class == "5�" && _subject == "�������� ��������� 5 �����")
        {
            // 1 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5�" && _subject == "������� ����������� 5 �����")
        {
            // 2 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ������� 5 �����")
        {
            // 3 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ��������� 5 �����")
        {
            // 4 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/QuestionnaireForParents/fromunity.php";
        }
        //5� ������ ������
        else if (_class == "5�" && _subject == "�������� ��������� 5 �����")
        {
            // 5 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5�" && _subject == "������� ����������� 5 �����")
        {
            // 6 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ������� 5 �����")
        {
            // 7 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ��������� 5 �����")
        {
            // 8 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/QuestionnaireForParents/fromunity.php";
        }
        //5� ������ ������
        else if (_class == "5�" && _subject == "�������� ��������� 5 �����")
        {
            // 9 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5�" && _subject == "������� ����������� 5 �����")
        {
            // 10 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ������� 5 �����")
        {
            // 11 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ��������� 5 �����")
        {
            // 12 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/QuestionnaireForParents/fromunity.php";
        }
        //5� ������ ������
        else if (_class == "5�" && _subject == "�������� ��������� 5 �����")
        {
            // 13 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5�" && _subject == "������� ����������� 5 �����")
        {
            // 14 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ������� 5 �����")
        {
            // 15 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ��������� 5 �����")
        {
            // 16 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/QuestionnaireForParents/fromunity.php";
        }
        //5� ������ ������
        else if (_class == "5�" && _subject == "�������� ��������� 5 �����")
        {
            // 17 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5�" && _subject == "������� ����������� 5 �����")
        {
            // 18 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ������� 5 �����")
        {
            // 19 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5�" && _subject == "�������� ��� ��������� 5 �����")
        {
            // 20 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/QuestionnaireForParents/fromunity.php";
        }

        // 9 ������

        //9� ������ ������
        else if (_class == "9�" && _subject == "����� ���")
        {
            // 1 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/TheDDOForm/fromunity.php";
        }
        else if (_class == "9�" && _subject == "����� ���������")
        {
            // 2 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ��������")
        {
            // 3 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ���������")
        {
            // 4 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ��������������")
        {
            // 5 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ������������")
        {
            // 6 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/TypeOfTemperament/fromunity.php";
        }
        //9� ������ ������
        else if (_class == "9�" && _subject == "����� ���")
        {
            // 7 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/TheDDOForm/fromunity.php";
        }
        else if (_class == "9�" && _subject == "����� ���������")
        {
            // 8 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ��������")
        {
            // 9 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ���������")
        {
            // 10 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ��������������")
        {
            // 11 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ������������")
        {
            // 12 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/TypeOfTemperament/fromunity.php";
        }
        //9� ������ ������
        else if (_class == "9�" && _subject == "����� ���")
        {
            // 13 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/TheDDOForm/fromunity.php";
        }
        else if (_class == "9�" && _subject == "����� ���������")
        {
            // 14 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ��������")
        {
            // 15 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ���������")
        {
            // 16 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ��������������")
        {
            // 17 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ������������")
        {
            // 18 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/TypeOfTemperament/fromunity.php";
        }
        //9� ������ ������
        else if (_class == "9�" && _subject == "����� ���")
        {
            // 19 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/TheDDOForm/fromunity.php";
        }
        else if (_class == "9�" && _subject == "����� ���������")
        {
            // 20 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ��������")
        {
            // 21 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ���������")
        {
            // 22 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ��������������")
        {
            // 23 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ������������")
        {
            // 24 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/TypeOfTemperament/fromunity.php";
        }
        //9� ������ ������
        else if (_class == "9�" && _subject == "����� ���")
        {
            // 25 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/TheDDOForm/fromunity.php";
        }
        else if (_class == "9�" && _subject == "����� ���������")
        {
            // 26 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ��������")
        {
            // 27 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ���������")
        {
            // 28 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9�" && _subject == "���� ��������������")
        {
            // 29 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9�" && _subject == "��� ������������")
        {
            // 30 ������
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/TypeOfTemperament/fromunity.php";
        }
        else
        {
            _url = "_subject Note";
        }
    }
    private void Start()
    {
        _childrenActivization = false;
        _answerText = false;
        _nameText = GameObject.FindGameObjectWithTag("Name").GetComponent<InputField>();
        _nameTextChildren = GameObject.FindGameObjectWithTag("NameChildren").GetComponent<InputField>(); 
        _results = GameObject.FindGameObjectsWithTag("Result");
        Panels();
    }
    public void AnswerText()
    {
        if (_answer != "")
        {
            _answerText = true;
        }
        else if (_answer == "")
        {
            _answerText = false;
        }
    }
    [Obsolete]
    private void Result()
    {
        for (int i = 0; i < _results.Length; i++)
        {
            if (_results[i].active == true)
            {
                _results[i].GetComponent<Text>().text = _allInformation;
            }
        }
            //_result.text = _allInformation;
    }
    [Obsolete]
    private void FixedUpdate()
    {
        _name = _nameText.text;
        _nameChildren = _nameTextChildren.text;
        AnswerText();
        if (_childrenActivization == false)
        {
            _allInformation = $"�������: ����������\n\n���: {_name}\n�����: {_class}\n������������ ��������: {_subject}\n��������� �����: {_answer}\n�����: {_scores.ToString()}\n���� ����������: ";

        }
        else if (_childrenActivization == true)
        {
            _allInformation = $"�������: ����������\n\n���: {_name}\n��� ������: {_nameChildren}\n�����: {_class}\n������������ ��������: {_subject}\n��������� �����: {_answer}\n�����: {_scores.ToString()}\n���� ����������: ";

        }
        Result();
        URL();
    }
    public void ChildrenActivization()
    {
        _childrenActivization = true;
    }
    public void Scores1()
    {
        _scores += 1;
    }
    public void Scores2()
    {
        _scores += 2;
    }
    public void Scores3()
    {
        _scores += 3;
    }
    public void Scores4()
    {
        _scores += 4;
    }
    public void Scores5()
    {
        _scores += 5;
    }
    public void Class5()
    {
        _class = "5";
    }
    public void Class9()
    {
        _class = "9";
    }
    public void �()
    {
        _class += "�";
        _classCategory = "�";
    }
    public void �()
    {
        _class += "�";
        _classCategory = "�";
    }
    public void �()
    {
        _class += "�";
        _classCategory = "�";
    }
    public void �()
    {
        _class += "�";
        _classCategory = "�";
    }
    public void �()
    {
        _class += "�";
        _classCategory = "�";
    }
    public void Class5Category1()
    {
        _subject = "�������� ��������� 5 �����";
    }
    public void Class5Category2()
    {
        _subject = "������� ����������� 5 �����";
    }
    public void Class5Category3()
    {
        _subject = "�������� ��� ������� 5 �����";
    }
    public void Class5Category4()
    {
        _subject = "�������� ��� ��������� 5 �����";
    }
    public void Class9Category1()
    {
        _subject = "����� ���";
    }
    public void Class9Category2()
    {
        _subject = "����� ���������";
    }
    public void Class9Category3()
    {
        _subject = "��� ��������";
    }
    public void Class9Category4()
    {
        _subject = "���� ���������";
    }
    public void Class9Category5()
    {
        _subject = "���� ��������������";
    }
    public void Class9Category6()
    {
        _subject = "��� ������������";
    }

    public void Panels()
    {
        if (_allObjects != null)
        {
            for (int i = 0; i < _allObjects.Length; i++)
            {
                _allObjects[i].SetActive(false);
            }
        }
    }
}
