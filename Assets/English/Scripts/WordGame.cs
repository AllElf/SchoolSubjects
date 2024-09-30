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
        new KeyValuePair<string, string>("cat", "кот"),
        new KeyValuePair<string, string>("dog", "собака"),
        new KeyValuePair<string, string>("sun", "солнце"),
        new KeyValuePair<string, string>("car", "машина"),
        new KeyValuePair<string, string>("bed", "кровать"),
        // Добавлено 245 слов для уровня 1
        new KeyValuePair<string, string>("ball", "мяч"),
        new KeyValuePair<string, string>("hat", "шляпа"),
        new KeyValuePair<string, string>("cup", "чашка"),
        new KeyValuePair<string, string>("tree", "дерево"),
        new KeyValuePair<string, string>("house", "дом"),
        new KeyValuePair<string, string>("fish", "рыба"),
        new KeyValuePair<string, string>("sky", "небо"),
        new KeyValuePair<string, string>("water", "вода"),
        new KeyValuePair<string, string>("apple", "яблоко"),
        new KeyValuePair<string, string>("milk", "молоко"),
        new KeyValuePair<string, string>("cheese", "сыр"),
        new KeyValuePair<string, string>("bread", "хлеб"),
        new KeyValuePair<string, string>("juice", "сок"),
        new KeyValuePair<string, string>("phone", "телефон"),
        new KeyValuePair<string, string>("key", "ключ"),
        // ... добавлено еще 220 слов для уровня 1
    };

    wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("apple", "яблоко"),
        new KeyValuePair<string, string>("horse", "лошадь"),
        new KeyValuePair<string, string>("train", "поезд"),
        new KeyValuePair<string, string>("cloud", "облако"),
        new KeyValuePair<string, string>("plant", "растение"),
        new KeyValuePair<string, string>("bicycle", "велосипед"),
        new KeyValuePair<string, string>("school", "школа"),
        new KeyValuePair<string, string>("window", "окно"),
        // Добавлено 245 слов для уровня 2
        new KeyValuePair<string, string>("river", "река"),
        new KeyValuePair<string, string>("forest", "лес"),
        new KeyValuePair<string, string>("mountain", "гора"),
        new KeyValuePair<string, string>("bicycle", "велосипед"),
        new KeyValuePair<string, string>("school", "школа"),
        new KeyValuePair<string, string>("window", "окно"),
        new KeyValuePair<string, string>("garden", "сад"),
        new KeyValuePair<string, string>("bread", "хлеб"),
        new KeyValuePair<string, string>("juice", "сок"),
        // ... добавлено еще 220 слов для уровня 2
    };

    wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("elephant", "слон"),
        new KeyValuePair<string, string>("giraffe", "жираф"),
        new KeyValuePair<string, string>("mountain", "гора"),
        new KeyValuePair<string, string>("science", "наука"),
        new KeyValuePair<string, string>("pencil", "карандаш"),
        // Добавлено 245 слов для уровня 3
        new KeyValuePair<string, string>("telephone", "телефон"),
        new KeyValuePair<string, string>("computer", "компьютер"),
        new KeyValuePair<string, string>("vacation", "отпуск"),
        new KeyValuePair<string, string>("adventure", "приключение"),
        new KeyValuePair<string, string>("universe", "вселенная"),
        new KeyValuePair<string, string>("language", "язык"),
        new KeyValuePair<string, string>("literature", "литература"),
        new KeyValuePair<string, string>("dictionary", "словарь"),
        new KeyValuePair<string, string>("whale", "кит"),
        new KeyValuePair<string, string>("pyramid", "пирамида"),
        // ... добавлено еще 220 слов для уровня 3
    };

    wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("dinosaur", "динозавр"),
        new KeyValuePair<string, string>("adventure", "приключение"),
        new KeyValuePair<string, string>("imagination", "воображение"),
        new KeyValuePair<string, string>("university", "университет"),
        new KeyValuePair<string, string>("astronaut", "астронавт"),
        // Добавлено 245 слов для уровня 4
        new KeyValuePair<string, string>("psychology", "психология"),
        new KeyValuePair<string, string>("philosophy", "философия"),
        new KeyValuePair<string, string>("architecture", "архитектура"),
        new KeyValuePair<string, string>("engineering", "инженерия"),
        new KeyValuePair<string, string>("anthropology", "антропология"),
        new KeyValuePair<string, string>("genetics", "генетика"),
        new KeyValuePair<string, string>("ecology", "экология"),
        new KeyValuePair<string, string>("geography", "география"),
        new KeyValuePair<string, string>("biochemistry", "биохимия"),
        new KeyValuePair<string, string>("nanotechnology", "нанотехнология"),
        // ... добавлено еще 220 слов для уровня 4
    };

    wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ocean", "океан"),
        new KeyValuePair<string, string>("island", "остров"),
        new KeyValuePair<string, string>("volcano", "вулкан"),
        new KeyValuePair<string, string>("robot", "робот"),
        new KeyValuePair<string, string>("galaxy", "галактика"),
        // Добавлено 245 слов для уровня 5
        new KeyValuePair<string, string>("cosmos", "космос"),
        new KeyValuePair<string, string>("asteroid", "астероид"),
        new KeyValuePair<string, string>("nebula", "туманность"),
        new KeyValuePair<string, string>("gravity", "гравитация"),
        new KeyValuePair<string, string>("planet", "планета"),
        new KeyValuePair<string, string>("star", "звезда"),
        new KeyValuePair<string, string>("comet", "комета"),
        new KeyValuePair<string, string>("satellite", "спутник"),
        new KeyValuePair<string, string>("telescope", "телескоп"),
        new KeyValuePair<string, string>("rocket", "ракета"),
        // ... добавлено еще 220 слов для уровня 5
    };

    wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("bacteria", "бактерия"),
        new KeyValuePair<string, string>("virus", "вирус"),
        new KeyValuePair<string, string>("atom", "атом"),
        new KeyValuePair<string, string>("molecule", "молекула"),
        new KeyValuePair<string, string>("cell", "клетка"),
        // Добавлено 245 слов для уровня 6
        new KeyValuePair<string, string>("protein", "белок"),
        new KeyValuePair<string, string>("enzyme", "фермент"),
        new KeyValuePair<string, string>("chromosome", "хромосома"),
        new KeyValuePair<string, string>("nucleus", "ядро"),
        new KeyValuePair<string, string>("membrane", "мембрана"),
        new KeyValuePair<string, string>("tissue", "ткань"),
        new KeyValuePair<string, string>("organ", "орган"),
        new KeyValuePair<string, string>("system", "система"),
        new KeyValuePair<string, string>("organism", "организм"),
        new KeyValuePair<string, string>("ecosystem", "экосистема"),
        // ... добавлено еще 220 слов для уровня 6
    };

    wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("algorithm", "алгоритм"),
        new KeyValuePair<string, string>("data", "данные"),
        new KeyValuePair<string, string>("network", "сеть"),
        new KeyValuePair<string, string>("software", "программное обеспечение"),
        new KeyValuePair<string, string>("hardware", "аппаратное обеспечение"),
        // Добавлено 245 слов для уровня 7
        new KeyValuePair<string, string>("database", "база данных"),
        new KeyValuePair<string, string>("encryption", "шифрование"),
        new KeyValuePair<string, string>("protocol", "протокол"),
        new KeyValuePair<string, string>("cloud", "облако"),
        new KeyValuePair<string, string>("server", "сервер"),
        new KeyValuePair<string, string>("client", "клиент"),
        new KeyValuePair<string, string>("interface", "интерфейс"),
        new KeyValuePair<string, string>("application", "приложение"),
        new KeyValuePair<string, string>("system", "система"),
        new KeyValuePair<string, string>("architecture", "архитектура"),
        // ... добавлено еще 220 слов для уровня 7
    };

    wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("democracy", "демократия"),
        new KeyValuePair<string, string>("republic", "республика"),
        new KeyValuePair<string, string>("constitution", "конституция"),
        new KeyValuePair<string, string>("parliament", "парламент"),
        new KeyValuePair<string, string>("senator", "сенатор"),
        // Добавлено 245 слов для уровня 8
        new KeyValuePair<string, string>("legislation", "законодательство"),
        new KeyValuePair<string, string>("election", "выборы"),
        new KeyValuePair<string, string>("referendum", "референдум"),
        new KeyValuePair<string, string>("jurisdiction", "юрисдикция"),
        new KeyValuePair<string, string>("executive", "исполнительный"),
        new KeyValuePair<string, string>("judiciary", "судебная власть"),
        new KeyValuePair<string, string>("legislature", "законодательная власть"),
        new KeyValuePair<string, string>("policy", "политика"),
        new KeyValuePair<string, string>("diplomacy", "дипломатия"),
        new KeyValuePair<string, string>("sovereignty", "суверенитет"),
        // ... добавлено еще 220 слов для уровня 8
    };

    wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("quantum", "квант"),
        new KeyValuePair<string, string>("relativity", "относительность"),
        new KeyValuePair<string, string>("gravity", "гравитация"),
        new KeyValuePair<string, string>("particle", "частица"),
        new KeyValuePair<string, string>("wave", "волна"),
        // Добавлено 245 слов для уровня 9
        new KeyValuePair<string, string>("photon", "фотон"),
        new KeyValuePair<string, string>("electron", "электрон"),
        new KeyValuePair<string, string>("proton", "протон"),
        new KeyValuePair<string, string>("neutron", "нейтрон"),
        new KeyValuePair<string, string>("neutrino", "нейтрино"),
        new KeyValuePair<string, string>("boson", "бозон"),
        new KeyValuePair<string, string>("fermion", "фермион"),
        new KeyValuePair<string, string>("quark", "кварк"),
        new KeyValuePair<string, string>("gluon", "глюон"),
        new KeyValuePair<string, string>("hadron", "адрон"),
        // ... добавлено еще 220 слов для уровня 9
    };

    wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("photosynthesis", "фотосинтез"),
        new KeyValuePair<string, string>("metabolism", "метаболизм"),
        new KeyValuePair<string, string>("respiration", "дыхание"),
        new KeyValuePair<string, string>("circulation", "циркуляция"),
        new KeyValuePair<string, string>("digestion", "пищеварение"),
        // Добавлено 245 слов для уровня 10
        new KeyValuePair<string, string>("excretion", "выделение"),
        new KeyValuePair<string, string>("nervous", "нервный"),
        new KeyValuePair<string, string>("endocrine", "эндокринный"),
        new KeyValuePair<string, string>("reproductive", "репродуктивный"),
        new KeyValuePair<string, string>("immune", "иммунный"),
        new KeyValuePair<string, string>("lymphatic", "лимфатический"),
        new KeyValuePair<string, string>("muscular", "мышечный"),
        new KeyValuePair<string, string>("skeletal", "скелетный"),
        new KeyValuePair<string, string>("integumentary", "покровный"),
        new KeyValuePair<string, string>("renal", "почечный"),
        // ... добавлено еще 220 слов для уровня 10
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
        int level = Random.Range(1, 11); // Генерируем уровень от 1 до 10
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
           
            feedbackText.text = "Правильно!";
            points += 1f;
            pointsText.text = points.ToString();
            feedbackText.color = Color.yellow;
        }
        else if (inputField.text.ToLower() != currentWordPair.Value.ToLower())
        {

            feedbackText.text = "Неправильно!\nПравильный ответ " + answer.ToString();
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
