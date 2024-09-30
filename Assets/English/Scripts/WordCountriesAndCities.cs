using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordCountriesAndCities : MonoBehaviour
{
    public Text wordDisplay;
    public InputField inputField;
    public Button submitButton;
    public Text feedbackText;
    public Text pointsText;

    [SerializeField] float points = 0;
    [SerializeField] string answer;
    private KeyValuePair<string, string> currentWordPair;
    private List<KeyValuePair<string, string>> wordsLevel1;
    private List<KeyValuePair<string, string>> wordsLevel2;
    private List<KeyValuePair<string, string>> wordsLevel3;
    private List<KeyValuePair<string, string>> wordsLevel4;
    private List<KeyValuePair<string, string>> wordsLevel5;
    private List<KeyValuePair<string, string>> wordsLevel6;
    private List<KeyValuePair<string, string>> wordsLevel7;
    private List<KeyValuePair<string, string>> wordsLevel8;
    private List<KeyValuePair<string, string>> wordsLevel9;
    private List<KeyValuePair<string, string>> wordsLevel10;

    [Header("Timer")]
    [SerializeField] Image _image;
    [SerializeField] float _seconds;
    [SerializeField] float fillAmounT = 1;
    [SerializeField] float _speed = 0.1f;

    void Start()
    {
        InitializeWords();
        GenerateWord();
        feedbackText.text = "";
        _seconds = _speed / _seconds;
        StartCoroutine(Clock());
        //submitButton.onClick.AddListener(CheckAnswer);
    }

    void InitializeWords()
    {
        wordsLevel1 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("paris", "�����"),
        new KeyValuePair<string, string>("london", "������"),
        new KeyValuePair<string, string>("berlin", "������"),
        new KeyValuePair<string, string>("madrid", "������"),
        new KeyValuePair<string, string>("rome", "���"),
        new KeyValuePair<string, string>("tokyo", "�����"),
        new KeyValuePair<string, string>("moscow", "������"),
        new KeyValuePair<string, string>("beijing", "�����"),
        new KeyValuePair<string, string>("sydney", "������"),
        new KeyValuePair<string, string>("new york", "���-����"),
        new KeyValuePair<string, string>("lisbon", "��������"),
        new KeyValuePair<string, string>("amsterdam", "���������"),
        new KeyValuePair<string, string>("vienna", "����"),
        new KeyValuePair<string, string>("dubai", "�����"),
        new KeyValuePair<string, string>("singapore", "��������"),
        new KeyValuePair<string, string>("istanbul", "�������"),
        new KeyValuePair<string, string>("bangkok", "�������"),
        new KeyValuePair<string, string>("hong kong", "�������"),
        new KeyValuePair<string, string>("seoul", "����"),
        new KeyValuePair<string, string>("los angeles", "���-��������"),
        // ... ��������� ��� 230 ���� ��� ������ 1
    };

        wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("canada", "������"),
        new KeyValuePair<string, string>("china", "�����"),
        new KeyValuePair<string, string>("brazil", "��������"),
        new KeyValuePair<string, string>("india", "�����"),
        new KeyValuePair<string, string>("russia", "������"),
        new KeyValuePair<string, string>("japan", "������"),
        new KeyValuePair<string, string>("australia", "���������"),
        new KeyValuePair<string, string>("mexico", "�������"),
        new KeyValuePair<string, string>("france", "�������"),
        new KeyValuePair<string, string>("germany", "��������"),
        new KeyValuePair<string, string>("italy", "������"),
        new KeyValuePair<string, string>("spain", "�������"),
        new KeyValuePair<string, string>("portugal", "����������"),
        new KeyValuePair<string, string>("netherlands", "����������"),
        new KeyValuePair<string, string>("belgium", "�������"),
        new KeyValuePair<string, string>("sweden", "������"),
        new KeyValuePair<string, string>("norway", "��������"),
        new KeyValuePair<string, string>("finland", "���������"),
        new KeyValuePair<string, string>("denmark", "�����"),
        new KeyValuePair<string, string>("switzerland", "���������"),
        // ... ��������� ��� 230 ���� ��� ������ 2
    };

        wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("santiago", "��������"),
        new KeyValuePair<string, string>("athens", "�����"),
        new KeyValuePair<string, string>("budapest", "��������"),
        new KeyValuePair<string, string>("cairo", "����"),
        new KeyValuePair<string, string>("dublin", "������"),
        new KeyValuePair<string, string>("helsinki", "���������"),
        new KeyValuePair<string, string>("jakarta", "��������"),
        new KeyValuePair<string, string>("lisbon", "��������"),
        new KeyValuePair<string, string>("oslo", "����"),
        new KeyValuePair<string, string>("prague", "�����"),
        new KeyValuePair<string, string>("reykjavik", "���������"),
        new KeyValuePair<string, string>("stockholm", "���������"),
        new KeyValuePair<string, string>("warsaw", "�������"),
        new KeyValuePair<string, string>("vienna", "����"),
        new KeyValuePair<string, string>("zagreb", "������"),
        new KeyValuePair<string, string>("abu dhabi", "���-����"),
        new KeyValuePair<string, string>("auckland", "������"),
        new KeyValuePair<string, string>("baku", "����"),
        new KeyValuePair<string, string>("bratislava", "����������"),
        new KeyValuePair<string, string>("bucharest", "��������"),
        // ... ��������� ��� 230 ���� ��� ������ 3
    };

        wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("malaysia", "��������"),
        new KeyValuePair<string, string>("thailand", "�������"),
        new KeyValuePair<string, string>("vietnam", "�������"),
        new KeyValuePair<string, string>("argentina", "���������"),
        new KeyValuePair<string, string>("colombia", "��������"),
        new KeyValuePair<string, string>("peru", "����"),
        new KeyValuePair<string, string>("chile", "����"),
        new KeyValuePair<string, string>("venezuela", "���������"),
        new KeyValuePair<string, string>("ecuador", "�������"),
        new KeyValuePair<string, string>("paraguay", "��������"),
        new KeyValuePair<string, string>("uruguay", "�������"),
        new KeyValuePair<string, string>("bolivia", "�������"),
        new KeyValuePair<string, string>("guatemala", "���������"),
        new KeyValuePair<string, string>("honduras", "��������"),
        new KeyValuePair<string, string>("nicaragua", "���������"),
        new KeyValuePair<string, string>("el salvador", "���������"),
        new KeyValuePair<string, string>("costa rica", "�����-����"),
        new KeyValuePair<string, string>("panama", "������"),
        new KeyValuePair<string, string>("jamaica", "������"),
        new KeyValuePair<string, string>("bahamas", "������"),
        // ... ��������� ��� 230 ���� ��� ������ 4
    };

        wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("athens", "�����"),
        new KeyValuePair<string, string>("baghdad", "������"),
        new KeyValuePair<string, string>("bangkok", "�������"),
        new KeyValuePair<string, string>("beijing", "�����"),
        new KeyValuePair<string, string>("berlin", "������"),
        new KeyValuePair<string, string>("brussels", "��������"),
        new KeyValuePair<string, string>("cairo", "����"),
        new KeyValuePair<string, string>("copenhagen", "����������"),
        new KeyValuePair<string, string>("dublin", "������"),
        new KeyValuePair<string, string>("havana", "������"),
        new KeyValuePair<string, string>("helsinki", "���������"),
        new KeyValuePair<string, string>("jakarta", "��������"),
        new KeyValuePair<string, string>("kathmandu", "��������"),
        new KeyValuePair<string, string>("kinshasa", "�������"),
        new KeyValuePair<string, string>("kuala lumpur", "�����-������"),
        new KeyValuePair<string, string>("lisbon", "��������"),
        new KeyValuePair<string, string>("luxembourg", "����������"),
        new KeyValuePair<string, string>("madrid", "������"),
        new KeyValuePair<string, string>("manila", "������"),
        new KeyValuePair<string, string>("mexico city", "������"),
        // ... ��������� ��� 230 ���� ��� ������ 5
    };

        wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("israel", "�������"),
        new KeyValuePair<string, string>("italy", "������"),
        new KeyValuePair<string, string>("iran", "����"),
        new KeyValuePair<string, string>("iraq", "����"),
        new KeyValuePair<string, string>("ireland", "��������"),
        new KeyValuePair<string, string>("jamaica", "������"),
        new KeyValuePair<string, string>("japan", "������"),
        new KeyValuePair<string, string>("jordan", "��������"),
        new KeyValuePair<string, string>("kazakhstan", "���������"),
        new KeyValuePair<string, string>("kenya", "�����"),
        new KeyValuePair<string, string>("kuwait", "������"),
        new KeyValuePair<string, string>("kyrgyzstan", "��������"),
        new KeyValuePair<string, string>("laos", "����"),
        new KeyValuePair<string, string>("latvia", "������"),
        new KeyValuePair<string, string>("lebanon", "�����"),
        new KeyValuePair<string, string>("liberia", "�������"),
        new KeyValuePair<string, string>("libya", "�����"),
        new KeyValuePair<string, string>("liechtenstein", "�����������"),
        new KeyValuePair<string, string>("lithuania", "�����"),
        new KeyValuePair<string, string>("luxembourg", "����������"),
        // ... ��������� ��� 230 ���� ��� ������ 6
    };

        wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("bucharest", "��������"),
        new KeyValuePair<string, string>("sofia", "�����"),
        new KeyValuePair<string, string>("belgrade", "�������"),
        new KeyValuePair<string, string>("zagreb", "������"),
        new KeyValuePair<string, string>("sarajevo", "�������"),
        new KeyValuePair<string, string>("ljubljana", "�������"),
        new KeyValuePair<string, string>("bratislava", "����������"),
        new KeyValuePair<string, string>("tbilisi", "�������"),
        new KeyValuePair<string, string>("yerevan", "������"),
        new KeyValuePair<string, string>("baku", "����"),
        new KeyValuePair<string, string>("minsk", "�����"),
        new KeyValuePair<string, string>("chisinau", "�������"),
        new KeyValuePair<string, string>("skopje", "������"),
        new KeyValuePair<string, string>("podgorica", "���������"),
        new KeyValuePair<string, string>("tirana", "������"),
        new KeyValuePair<string, string>("pristina", "��������"),
        new KeyValuePair<string, string>("vaduz", "�����"),
        new KeyValuePair<string, string>("andorra la vella", "�������-��-�����"),
        new KeyValuePair<string, string>("san marino", "���-������"),
        new KeyValuePair<string, string>("monaco", "������"),
        // ... ��������� ��� 230 ���� ��� ������ 7
    };

        wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("venezuela", "���������"),
        new KeyValuePair<string, string>("vietnam", "�������"),
        new KeyValuePair<string, string>("yemen", "�����"),
        new KeyValuePair<string, string>("zambia", "������"),
        new KeyValuePair<string, string>("zimbabwe", "��������"),
        new KeyValuePair<string, string>("afghanistan", "����������"),
        new KeyValuePair<string, string>("albania", "�������"),
        new KeyValuePair<string, string>("algeria", "�����"),
        new KeyValuePair<string, string>("andorra", "�������"),
        new KeyValuePair<string, string>("angola", "������"),
        new KeyValuePair<string, string>("antigua and barbuda", "������� � �������"),
        new KeyValuePair<string, string>("argentina", "���������"),
        new KeyValuePair<string, string>("armenia", "�������"),
        new KeyValuePair<string, string>("australia", "���������"),
        new KeyValuePair<string, string>("austria", "�������"),
        new KeyValuePair<string, string>("azerbaijan", "�����������"),
        new KeyValuePair<string, string>("bahamas", "������"),
        new KeyValuePair<string, string>("bahrain", "�������"),
        new KeyValuePair<string, string>("bangladesh", "���������"),
        new KeyValuePair<string, string>("barbados", "��������"),
        // ... ��������� ��� 230 ���� ��� ������ 8
    };

        wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("kazakhstan", "���������"),
        new KeyValuePair<string, string>("kenya", "�����"),
        new KeyValuePair<string, string>("kiribati", "��������"),
        new KeyValuePair<string, string>("korea", "�����"),
        new KeyValuePair<string, string>("kuwait", "������"),
        new KeyValuePair<string, string>("kyrgyzstan", "��������"),
        new KeyValuePair<string, string>("laos", "����"),
        new KeyValuePair<string, string>("latvia", "������"),
        new KeyValuePair<string, string>("lebanon", "�����"),
        new KeyValuePair<string, string>("lesotho", "������"),
        new KeyValuePair<string, string>("liberia", "�������"),
        new KeyValuePair<string, string>("libya", "�����"),
        new KeyValuePair<string, string>("liechtenstein", "�����������"),
        new KeyValuePair<string, string>("lithuania", "�����"),
        new KeyValuePair<string, string>("luxembourg", "����������"),
        new KeyValuePair<string, string>("macedonia", "���������"),
        new KeyValuePair<string, string>("madagascar", "����������"),
        new KeyValuePair<string, string>("malawi", "������"),
        new KeyValuePair<string, string>("malaysia", "��������"),
        new KeyValuePair<string, string>("maldives", "��������"),
        // ... ��������� ��� 230 ���� ��� ������ 9
    };

        wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("mauritania", "����������"),
        new KeyValuePair<string, string>("mauritius", "��������"),
        new KeyValuePair<string, string>("mexico", "�������"),
        new KeyValuePair<string, string>("moldova", "�������"),
        new KeyValuePair<string, string>("monaco", "������"),
        new KeyValuePair<string, string>("mongolia", "��������"),
        new KeyValuePair<string, string>("montenegro", "����������"),
        new KeyValuePair<string, string>("morocco", "�������"),
        new KeyValuePair<string, string>("mozambique", "��������"),
        new KeyValuePair<string, string>("myanmar", "������"),
        new KeyValuePair<string, string>("namibia", "�������"),
        new KeyValuePair<string, string>("nauru", "�����"),
        new KeyValuePair<string, string>("nepal", "�����"),
        new KeyValuePair<string, string>("netherlands", "����������"),
        new KeyValuePair<string, string>("new zealand", "����� ��������"),
        new KeyValuePair<string, string>("nicaragua", "���������"),
        new KeyValuePair<string, string>("niger", "�����"),
        new KeyValuePair<string, string>("nigeria", "�������"),
        new KeyValuePair<string, string>("norway", "��������"),
        new KeyValuePair<string, string>("oman", "����"),
        // ... ��������� ��� 230 ���� ��� ������ 10
    };
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
                fillAmounT = 1;
                CheckAnswer();
                GenerateWord();
            }
        }
    }

    public void GenerateWord()
    {
        inputField.text = "";
        int level = Random.Range(1, 11); // ���������� ������� �� 1 �� 10
        List<KeyValuePair<string, string>> wordList;

        switch (level)
        {
            case 1:
                wordList = wordsLevel1;
                break;
            case 2:
                wordList = wordsLevel2;
                break;
            case 3:
                wordList = wordsLevel3;
                break;
            case 4:
                wordList = wordsLevel4;
                break;
            case 5:
                wordList = wordsLevel5;
                break;
            case 6:
                wordList = wordsLevel6;
                break;
            case 7:
                wordList = wordsLevel7;
                break;
            case 8:
                wordList = wordsLevel8;
                break;
            case 9:
                wordList = wordsLevel9;
                break;
            case 10:
                wordList = wordsLevel10;
                break;
            default:
                wordList = new List<KeyValuePair<string, string>>();
                break;
        }

        int wordIndex = Random.Range(0, wordList.Count);
        currentWordPair = wordList[wordIndex];
        wordDisplay.text = currentWordPair.Key;
        answer = currentWordPair.Value.ToLower();
        fillAmounT = 1;
        //feedbackText.text = "";
    }


    public void CheckAnswer()
    {

        if (inputField.text.ToLower() == answer)
        {

            feedbackText.text = "���������!";
            points += 1f;
            pointsText.text = points.ToString();
            feedbackText.color = Color.yellow;
        }
        else if (inputField.text.ToLower() != currentWordPair.Value.ToLower())
        {

            feedbackText.text = "�����������!\n���������� ����� " + answer.ToString();
            points -= 1f;
            if (points <= 0)
            {
                points = 0;
            }
            pointsText.text = points.ToString();
            feedbackText.color = Color.red;



        }
    }
}
