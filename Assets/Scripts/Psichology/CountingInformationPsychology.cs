using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CountingInformationPsychology : MonoBehaviour
{
    [Header("Общие переменные для Имени Родителя/Ученика")]
    [SerializeField] InputField _nameText; //Текстовое поле для имени
    [SerializeField] InputField _nameTextChildren;// Текстовое поле для имени ребёнка
    [Header("Панели для активации")]
    [SerializeField] GameObject[] _allObjects;
    [SerializeField] GameObject[] _results; // Текстовые поля с результатами;
    [Header("ФИО")]
    [SerializeField] string _name; // Фамилия и имя
    [Header("ФИО реьёнка")]
    [SerializeField] string _nameChildren; // Фамилия и имя ребёнка
    [Header("Класс")]
    [SerializeField] string _class; // Класс
    [Header("Категория класса")]
    [SerializeField] string _classCategory; // Категория класса
    [Header("Предмет")]
    [SerializeField] string _subject; // Предмет и подпредмет
    [Header("Текстовый ответ")]
    [SerializeField] string _answer; // Ответ
    [Header("Общая информация")]
    [Multiline][SerializeField] string _allInformation; // Общая информация
    [Header("Родитель/Ученик")]
    [SerializeField] bool _childrenActivization;
    [Header("Имеется текстовый ответ")]
    [SerializeField] bool _answerText;
    [Header("Баллы")]
    [SerializeField] int _scores = 0; // Баллы

    [Header("Сылка для класса и направления")]
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
        // 5 классы

        //5А классы ссылки
        if (_class == "5А" && _subject == "Школьная мотивация 5 класс")
        {
            // 1 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5А" && _subject == "Уровень тревожности 5 класс")
        {
            // 2 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5А" && _subject == "Опросник для учителя 5 класс")
        {
            // 3 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5А" && _subject == "Опросник для родителей 5 класс")
        {
            // 4 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5A/QuestionnaireForParents/fromunity.php";
        }
        //5Б классы ссылки
        else if (_class == "5Б" && _subject == "Школьная мотивация 5 класс")
        {
            // 5 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5Б" && _subject == "Уровень тревожности 5 класс")
        {
            // 6 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5Б" && _subject == "Опросник для учителя 5 класс")
        {
            // 7 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5Б" && _subject == "Опросник для родителей 5 класс")
        {
            // 8 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5B/QuestionnaireForParents/fromunity.php";
        }
        //5В классы ссылки
        else if (_class == "5В" && _subject == "Школьная мотивация 5 класс")
        {
            // 9 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5В" && _subject == "Уровень тревожности 5 класс")
        {
            // 10 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5В" && _subject == "Опросник для учителя 5 класс")
        {
            // 11 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5В" && _subject == "Опросник для родителей 5 класс")
        {
            // 12 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5V/QuestionnaireForParents/fromunity.php";
        }
        //5Г классы ссылки
        else if (_class == "5Г" && _subject == "Школьная мотивация 5 класс")
        {
            // 13 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5Г" && _subject == "Уровень тревожности 5 класс")
        {
            // 14 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5Г" && _subject == "Опросник для учителя 5 класс")
        {
            // 15 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5Г" && _subject == "Опросник для родителей 5 класс")
        {
            // 16 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5G/QuestionnaireForParents/fromunity.php";
        }
        //5Д классы ссылки
        else if (_class == "5Д" && _subject == "Школьная мотивация 5 класс")
        {
            // 17 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/SchoolMotivation/fromunity.php";
        }
        else if (_class == "5Д" && _subject == "Уровень тревожности 5 класс")
        {
            // 18 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/AnxietyLevel/fromunity.php";
        }
        else if (_class == "5Д" && _subject == "Опросник для учителя 5 класс")
        {
            // 19 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/QuestionnaireForTeachers/fromunity.php";
        }
        else if (_class == "5Д" && _subject == "Опросник для родителей 5 класс")
        {
            // 20 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/5D/QuestionnaireForParents/fromunity.php";
        }

        // 9 классы

        //9А классы ссылки
        else if (_class == "9А" && _subject == "Бланк ДДО")
        {
            // 1 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/TheDDOForm/fromunity.php";
        }
        else if (_class == "9А" && _subject == "Карта интересов")
        {
            // 2 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9А" && _subject == "КОС Опросник")
        {
            // 3 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9А" && _subject == "Проф мотивация")
        {
            // 4 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9А" && _subject == "Тест Профсклонности")
        {
            // 5 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9А" && _subject == "Тип темперамента")
        {
            // 6 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9A/TypeOfTemperament/fromunity.php";
        }
        //9Б классы ссылки
        else if (_class == "9Б" && _subject == "Бланк ДДО")
        {
            // 7 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/TheDDOForm/fromunity.php";
        }
        else if (_class == "9Б" && _subject == "Карта интересов")
        {
            // 8 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9Б" && _subject == "КОС Опросник")
        {
            // 9 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9Б" && _subject == "Проф мотивация")
        {
            // 10 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9Б" && _subject == "Тест Профсклонности")
        {
            // 11 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9Б" && _subject == "Тип темперамента")
        {
            // 12 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9B/TypeOfTemperament/fromunity.php";
        }
        //9В классы ссылки
        else if (_class == "9В" && _subject == "Бланк ДДО")
        {
            // 13 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/TheDDOForm/fromunity.php";
        }
        else if (_class == "9В" && _subject == "Карта интересов")
        {
            // 14 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9В" && _subject == "КОС Опросник")
        {
            // 15 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9В" && _subject == "Проф мотивация")
        {
            // 16 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9В" && _subject == "Тест Профсклонности")
        {
            // 17 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9В" && _subject == "Тип темперамента")
        {
            // 18 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9V/TypeOfTemperament/fromunity.php";
        }
        //9Г классы ссылки
        else if (_class == "9Г" && _subject == "Бланк ДДО")
        {
            // 19 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/TheDDOForm/fromunity.php";
        }
        else if (_class == "9Г" && _subject == "Карта интересов")
        {
            // 20 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9Г" && _subject == "КОС Опросник")
        {
            // 21 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9Г" && _subject == "Проф мотивация")
        {
            // 22 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9Г" && _subject == "Тест Профсклонности")
        {
            // 23 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9Г" && _subject == "Тип темперамента")
        {
            // 24 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9G/TypeOfTemperament/fromunity.php";
        }
        //9Д классы ссылки
        else if (_class == "9Д" && _subject == "Бланк ДДО")
        {
            // 25 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/TheDDOForm/fromunity.php";
        }
        else if (_class == "9Д" && _subject == "Карта интересов")
        {
            // 26 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/TheMapOfInterests/fromunity.php";
        }
        else if (_class == "9Д" && _subject == "КОС Опросник")
        {
            // 27 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/CBSQuestionnaire/fromunity.php";
        }
        else if (_class == "9Д" && _subject == "Проф мотивация")
        {
            // 28 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/ProfessionalMotivation/fromunity.php";
        }
        else if (_class == "9Д" && _subject == "Тест Профсклонности")
        {
            // 29 ссылка
            _url = "https://sehriyospace.uz/Unity/Psychology/9D/ProfessionalAptitudeTest/fromunity.php";
        }
        else if (_class == "9Д" && _subject == "Тип темперамента")
        {
            // 30 ссылка
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
            _allInformation = $"Предмет: Психология\n\nФИО: {_name}\nКласс: {_class}\nПодкатегория предмета: {_subject}\nТекстовый ответ: {_answer}\nБаллы: {_scores.ToString()}\nДата выполнения: ";

        }
        else if (_childrenActivization == true)
        {
            _allInformation = $"Предмет: психология\n\nФИО: {_name}\nФИО ребёнка: {_nameChildren}\nКласс: {_class}\nПодкатегория предмета: {_subject}\nТекстовый ответ: {_answer}\nБаллы: {_scores.ToString()}\nДата выполнения: ";

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
    public void А()
    {
        _class += "А";
        _classCategory = "А";
    }
    public void Б()
    {
        _class += "Б";
        _classCategory = "Б";
    }
    public void В()
    {
        _class += "В";
        _classCategory = "В";
    }
    public void Г()
    {
        _class += "Г";
        _classCategory = "Г";
    }
    public void Д()
    {
        _class += "Д";
        _classCategory = "Д";
    }
    public void Class5Category1()
    {
        _subject = "Школьная мотивация 5 класс";
    }
    public void Class5Category2()
    {
        _subject = "Уровень тревожности 5 класс";
    }
    public void Class5Category3()
    {
        _subject = "Опросник для учителя 5 класс";
    }
    public void Class5Category4()
    {
        _subject = "Опросник для родителей 5 класс";
    }
    public void Class9Category1()
    {
        _subject = "Бланк ДДО";
    }
    public void Class9Category2()
    {
        _subject = "Карта интересов";
    }
    public void Class9Category3()
    {
        _subject = "КОС Опросник";
    }
    public void Class9Category4()
    {
        _subject = "Проф мотивация";
    }
    public void Class9Category5()
    {
        _subject = "Тест Профсклонности";
    }
    public void Class9Category6()
    {
        _subject = "Тип темперамента";
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
