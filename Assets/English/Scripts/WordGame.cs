using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
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
        new KeyValuePair<string, string>("cat", "���"),
        new KeyValuePair<string, string>("dog", "������"),
        new KeyValuePair<string, string>("sun", "������"),
        new KeyValuePair<string, string>("car", "������"),
        new KeyValuePair<string, string>("bed", "�������"),
        // ��������� 245 ���� ��� ������ 1
        new KeyValuePair<string, string>("ball", "���"),
        new KeyValuePair<string, string>("hat", "�����"),
        new KeyValuePair<string, string>("cup", "�����"),
        new KeyValuePair<string, string>("tree", "������"),
        new KeyValuePair<string, string>("house", "���"),
        new KeyValuePair<string, string>("fish", "����"),
        new KeyValuePair<string, string>("sky", "����"),
        new KeyValuePair<string, string>("water", "����"),
        new KeyValuePair<string, string>("apple", "������"),
        new KeyValuePair<string, string>("milk", "������"),
        new KeyValuePair<string, string>("cheese", "���"),
        new KeyValuePair<string, string>("bread", "����"),
        new KeyValuePair<string, string>("juice", "���"),
        new KeyValuePair<string, string>("phone", "�������"),
        new KeyValuePair<string, string>("key", "����"),
        // ... ��������� ��� 220 ���� ��� ������ 1
    };

    wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("apple", "������"),
        new KeyValuePair<string, string>("horse", "������"),
        new KeyValuePair<string, string>("train", "�����"),
        new KeyValuePair<string, string>("cloud", "������"),
        new KeyValuePair<string, string>("plant", "��������"),
        new KeyValuePair<string, string>("bicycle", "���������"),
        new KeyValuePair<string, string>("school", "�����"),
        new KeyValuePair<string, string>("window", "����"),
        // ��������� 245 ���� ��� ������ 2
        new KeyValuePair<string, string>("river", "����"),
        new KeyValuePair<string, string>("forest", "���"),
        new KeyValuePair<string, string>("mountain", "����"),
        new KeyValuePair<string, string>("bicycle", "���������"),
        new KeyValuePair<string, string>("school", "�����"),
        new KeyValuePair<string, string>("window", "����"),
        new KeyValuePair<string, string>("garden", "���"),
        new KeyValuePair<string, string>("bread", "����"),
        new KeyValuePair<string, string>("juice", "���"),
        // ... ��������� ��� 220 ���� ��� ������ 2
    };

    wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("elephant", "����"),
        new KeyValuePair<string, string>("giraffe", "�����"),
        new KeyValuePair<string, string>("mountain", "����"),
        new KeyValuePair<string, string>("science", "�����"),
        new KeyValuePair<string, string>("pencil", "��������"),
        // ��������� 245 ���� ��� ������ 3
        new KeyValuePair<string, string>("telephone", "�������"),
        new KeyValuePair<string, string>("computer", "���������"),
        new KeyValuePair<string, string>("vacation", "������"),
        new KeyValuePair<string, string>("adventure", "�����������"),
        new KeyValuePair<string, string>("universe", "���������"),
        new KeyValuePair<string, string>("language", "����"),
        new KeyValuePair<string, string>("literature", "����������"),
        new KeyValuePair<string, string>("dictionary", "�������"),
        new KeyValuePair<string, string>("whale", "���"),
        new KeyValuePair<string, string>("pyramid", "��������"),
        // ... ��������� ��� 220 ���� ��� ������ 3
    };

    wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("dinosaur", "��������"),
        new KeyValuePair<string, string>("adventure", "�����������"),
        new KeyValuePair<string, string>("imagination", "�����������"),
        new KeyValuePair<string, string>("university", "�����������"),
        new KeyValuePair<string, string>("astronaut", "���������"),
        // ��������� 245 ���� ��� ������ 4
        new KeyValuePair<string, string>("psychology", "����������"),
        new KeyValuePair<string, string>("philosophy", "���������"),
        new KeyValuePair<string, string>("architecture", "�����������"),
        new KeyValuePair<string, string>("engineering", "���������"),
        new KeyValuePair<string, string>("anthropology", "������������"),
        new KeyValuePair<string, string>("genetics", "��������"),
        new KeyValuePair<string, string>("ecology", "��������"),
        new KeyValuePair<string, string>("geography", "���������"),
        new KeyValuePair<string, string>("biochemistry", "��������"),
        new KeyValuePair<string, string>("nanotechnology", "��������������"),
        // ... ��������� ��� 220 ���� ��� ������ 4
    };

    wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ocean", "�����"),
        new KeyValuePair<string, string>("island", "������"),
        new KeyValuePair<string, string>("volcano", "������"),
        new KeyValuePair<string, string>("robot", "�����"),
        new KeyValuePair<string, string>("galaxy", "���������"),
        // ��������� 245 ���� ��� ������ 5
        new KeyValuePair<string, string>("cosmos", "������"),
        new KeyValuePair<string, string>("asteroid", "��������"),
        new KeyValuePair<string, string>("nebula", "����������"),
        new KeyValuePair<string, string>("gravity", "����������"),
        new KeyValuePair<string, string>("planet", "�������"),
        new KeyValuePair<string, string>("star", "������"),
        new KeyValuePair<string, string>("comet", "������"),
        new KeyValuePair<string, string>("satellite", "�������"),
        new KeyValuePair<string, string>("telescope", "��������"),
        new KeyValuePair<string, string>("rocket", "������"),
        // ... ��������� ��� 220 ���� ��� ������ 5
    };

    wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("bacteria", "��������"),
        new KeyValuePair<string, string>("virus", "�����"),
        new KeyValuePair<string, string>("atom", "����"),
        new KeyValuePair<string, string>("molecule", "��������"),
        new KeyValuePair<string, string>("cell", "������"),
        // ��������� 245 ���� ��� ������ 6
        new KeyValuePair<string, string>("protein", "�����"),
        new KeyValuePair<string, string>("enzyme", "�������"),
        new KeyValuePair<string, string>("chromosome", "���������"),
        new KeyValuePair<string, string>("nucleus", "����"),
        new KeyValuePair<string, string>("membrane", "��������"),
        new KeyValuePair<string, string>("tissue", "�����"),
        new KeyValuePair<string, string>("organ", "�����"),
        new KeyValuePair<string, string>("system", "�������"),
        new KeyValuePair<string, string>("organism", "��������"),
        new KeyValuePair<string, string>("ecosystem", "����������"),
        // ... ��������� ��� 220 ���� ��� ������ 6
    };

    wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("algorithm", "��������"),
        new KeyValuePair<string, string>("data", "������"),
        new KeyValuePair<string, string>("network", "����"),
        new KeyValuePair<string, string>("software", "����������� �����������"),
        new KeyValuePair<string, string>("hardware", "���������� �����������"),
        // ��������� 245 ���� ��� ������ 7
        new KeyValuePair<string, string>("database", "���� ������"),
        new KeyValuePair<string, string>("encryption", "����������"),
        new KeyValuePair<string, string>("protocol", "��������"),
        new KeyValuePair<string, string>("cloud", "������"),
        new KeyValuePair<string, string>("server", "������"),
        new KeyValuePair<string, string>("client", "������"),
        new KeyValuePair<string, string>("interface", "���������"),
        new KeyValuePair<string, string>("application", "����������"),
        new KeyValuePair<string, string>("system", "�������"),
        new KeyValuePair<string, string>("architecture", "�����������"),
        // ... ��������� ��� 220 ���� ��� ������ 7
    };

    wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("democracy", "����������"),
        new KeyValuePair<string, string>("republic", "����������"),
        new KeyValuePair<string, string>("constitution", "�����������"),
        new KeyValuePair<string, string>("parliament", "���������"),
        new KeyValuePair<string, string>("senator", "�������"),
        // ��������� 245 ���� ��� ������ 8
        new KeyValuePair<string, string>("legislation", "����������������"),
        new KeyValuePair<string, string>("election", "������"),
        new KeyValuePair<string, string>("referendum", "����������"),
        new KeyValuePair<string, string>("jurisdiction", "����������"),
        new KeyValuePair<string, string>("executive", "��������������"),
        new KeyValuePair<string, string>("judiciary", "�������� ������"),
        new KeyValuePair<string, string>("legislature", "��������������� ������"),
        new KeyValuePair<string, string>("policy", "��������"),
        new KeyValuePair<string, string>("diplomacy", "����������"),
        new KeyValuePair<string, string>("sovereignty", "�����������"),
        // ... ��������� ��� 220 ���� ��� ������ 8
    };

    wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("quantum", "�����"),
        new KeyValuePair<string, string>("relativity", "���������������"),
        new KeyValuePair<string, string>("gravity", "����������"),
        new KeyValuePair<string, string>("particle", "�������"),
        new KeyValuePair<string, string>("wave", "�����"),
        // ��������� 245 ���� ��� ������ 9
        new KeyValuePair<string, string>("photon", "�����"),
        new KeyValuePair<string, string>("electron", "��������"),
        new KeyValuePair<string, string>("proton", "������"),
        new KeyValuePair<string, string>("neutron", "�������"),
        new KeyValuePair<string, string>("neutrino", "��������"),
        new KeyValuePair<string, string>("boson", "�����"),
        new KeyValuePair<string, string>("fermion", "�������"),
        new KeyValuePair<string, string>("quark", "�����"),
        new KeyValuePair<string, string>("gluon", "�����"),
        new KeyValuePair<string, string>("hadron", "�����"),
        // ... ��������� ��� 220 ���� ��� ������ 9
    };

    wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("photosynthesis", "����������"),
        new KeyValuePair<string, string>("metabolism", "����������"),
        new KeyValuePair<string, string>("respiration", "�������"),
        new KeyValuePair<string, string>("circulation", "����������"),
        new KeyValuePair<string, string>("digestion", "�����������"),
        // ��������� 245 ���� ��� ������ 10
        new KeyValuePair<string, string>("excretion", "���������"),
        new KeyValuePair<string, string>("nervous", "�������"),
        new KeyValuePair<string, string>("endocrine", "�����������"),
        new KeyValuePair<string, string>("reproductive", "��������������"),
        new KeyValuePair<string, string>("immune", "��������"),
        new KeyValuePair<string, string>("lymphatic", "�������������"),
        new KeyValuePair<string, string>("muscular", "��������"),
        new KeyValuePair<string, string>("skeletal", "���������"),
        new KeyValuePair<string, string>("integumentary", "���������"),
        new KeyValuePair<string, string>("renal", "��������"),
        // ... ��������� ��� 220 ���� ��� ������ 10
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
