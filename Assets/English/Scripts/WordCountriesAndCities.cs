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
        new KeyValuePair<string, string>("paris", "Париж"),
        new KeyValuePair<string, string>("london", "Лондон"),
        new KeyValuePair<string, string>("berlin", "Берлин"),
        new KeyValuePair<string, string>("madrid", "Мадрид"),
        new KeyValuePair<string, string>("rome", "Рим"),
        new KeyValuePair<string, string>("tokyo", "Токио"),
        new KeyValuePair<string, string>("moscow", "Москва"),
        new KeyValuePair<string, string>("beijing", "Пекин"),
        new KeyValuePair<string, string>("sydney", "Сидней"),
        new KeyValuePair<string, string>("new york", "Нью-Йорк"),
        new KeyValuePair<string, string>("lisbon", "Лиссабон"),
        new KeyValuePair<string, string>("amsterdam", "Амстердам"),
        new KeyValuePair<string, string>("vienna", "Вена"),
        new KeyValuePair<string, string>("dubai", "Дубай"),
        new KeyValuePair<string, string>("singapore", "Сингапур"),
        new KeyValuePair<string, string>("istanbul", "Стамбул"),
        new KeyValuePair<string, string>("bangkok", "Бангкок"),
        new KeyValuePair<string, string>("hong kong", "Гонконг"),
        new KeyValuePair<string, string>("seoul", "Сеул"),
        new KeyValuePair<string, string>("los angeles", "Лос-Анджелес"),
        // ... добавлено еще 230 слов для уровня 1
    };

        wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("canada", "Канада"),
        new KeyValuePair<string, string>("china", "Китай"),
        new KeyValuePair<string, string>("brazil", "Бразилия"),
        new KeyValuePair<string, string>("india", "Индия"),
        new KeyValuePair<string, string>("russia", "Россия"),
        new KeyValuePair<string, string>("japan", "Япония"),
        new KeyValuePair<string, string>("australia", "Австралия"),
        new KeyValuePair<string, string>("mexico", "Мексика"),
        new KeyValuePair<string, string>("france", "Франция"),
        new KeyValuePair<string, string>("germany", "Германия"),
        new KeyValuePair<string, string>("italy", "Италия"),
        new KeyValuePair<string, string>("spain", "Испания"),
        new KeyValuePair<string, string>("portugal", "Португалия"),
        new KeyValuePair<string, string>("netherlands", "Нидерланды"),
        new KeyValuePair<string, string>("belgium", "Бельгия"),
        new KeyValuePair<string, string>("sweden", "Швеция"),
        new KeyValuePair<string, string>("norway", "Норвегия"),
        new KeyValuePair<string, string>("finland", "Финляндия"),
        new KeyValuePair<string, string>("denmark", "Дания"),
        new KeyValuePair<string, string>("switzerland", "Швейцария"),
        // ... добавлено еще 230 слов для уровня 2
    };

        wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("santiago", "Сантьяго"),
        new KeyValuePair<string, string>("athens", "Афины"),
        new KeyValuePair<string, string>("budapest", "Будапешт"),
        new KeyValuePair<string, string>("cairo", "Каир"),
        new KeyValuePair<string, string>("dublin", "Дублин"),
        new KeyValuePair<string, string>("helsinki", "Хельсинки"),
        new KeyValuePair<string, string>("jakarta", "Джакарта"),
        new KeyValuePair<string, string>("lisbon", "Лиссабон"),
        new KeyValuePair<string, string>("oslo", "Осло"),
        new KeyValuePair<string, string>("prague", "Прага"),
        new KeyValuePair<string, string>("reykjavik", "Рейкьявик"),
        new KeyValuePair<string, string>("stockholm", "Стокгольм"),
        new KeyValuePair<string, string>("warsaw", "Варшава"),
        new KeyValuePair<string, string>("vienna", "Вена"),
        new KeyValuePair<string, string>("zagreb", "Загреб"),
        new KeyValuePair<string, string>("abu dhabi", "Абу-Даби"),
        new KeyValuePair<string, string>("auckland", "Окленд"),
        new KeyValuePair<string, string>("baku", "Баку"),
        new KeyValuePair<string, string>("bratislava", "Братислава"),
        new KeyValuePair<string, string>("bucharest", "Бухарест"),
        // ... добавлено еще 230 слов для уровня 3
    };

        wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("malaysia", "Малайзия"),
        new KeyValuePair<string, string>("thailand", "Таиланд"),
        new KeyValuePair<string, string>("vietnam", "Вьетнам"),
        new KeyValuePair<string, string>("argentina", "Аргентина"),
        new KeyValuePair<string, string>("colombia", "Колумбия"),
        new KeyValuePair<string, string>("peru", "Перу"),
        new KeyValuePair<string, string>("chile", "Чили"),
        new KeyValuePair<string, string>("venezuela", "Венесуэла"),
        new KeyValuePair<string, string>("ecuador", "Эквадор"),
        new KeyValuePair<string, string>("paraguay", "Парагвай"),
        new KeyValuePair<string, string>("uruguay", "Уругвай"),
        new KeyValuePair<string, string>("bolivia", "Боливия"),
        new KeyValuePair<string, string>("guatemala", "Гватемала"),
        new KeyValuePair<string, string>("honduras", "Гондурас"),
        new KeyValuePair<string, string>("nicaragua", "Никарагуа"),
        new KeyValuePair<string, string>("el salvador", "Сальвадор"),
        new KeyValuePair<string, string>("costa rica", "Коста-Рика"),
        new KeyValuePair<string, string>("panama", "Панама"),
        new KeyValuePair<string, string>("jamaica", "Ямайка"),
        new KeyValuePair<string, string>("bahamas", "Багамы"),
        // ... добавлено еще 230 слов для уровня 4
    };

        wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("athens", "Афины"),
        new KeyValuePair<string, string>("baghdad", "Багдад"),
        new KeyValuePair<string, string>("bangkok", "Бангкок"),
        new KeyValuePair<string, string>("beijing", "Пекин"),
        new KeyValuePair<string, string>("berlin", "Берлин"),
        new KeyValuePair<string, string>("brussels", "Брюссель"),
        new KeyValuePair<string, string>("cairo", "Каир"),
        new KeyValuePair<string, string>("copenhagen", "Копенгаген"),
        new KeyValuePair<string, string>("dublin", "Дублин"),
        new KeyValuePair<string, string>("havana", "Гавана"),
        new KeyValuePair<string, string>("helsinki", "Хельсинки"),
        new KeyValuePair<string, string>("jakarta", "Джакарта"),
        new KeyValuePair<string, string>("kathmandu", "Катманду"),
        new KeyValuePair<string, string>("kinshasa", "Киншаса"),
        new KeyValuePair<string, string>("kuala lumpur", "Куала-Лумпур"),
        new KeyValuePair<string, string>("lisbon", "Лиссабон"),
        new KeyValuePair<string, string>("luxembourg", "Люксембург"),
        new KeyValuePair<string, string>("madrid", "Мадрид"),
        new KeyValuePair<string, string>("manila", "Манила"),
        new KeyValuePair<string, string>("mexico city", "Мехико"),
        // ... добавлено еще 230 слов для уровня 5
    };

        wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("israel", "Израиль"),
        new KeyValuePair<string, string>("italy", "Италия"),
        new KeyValuePair<string, string>("iran", "Иран"),
        new KeyValuePair<string, string>("iraq", "Ирак"),
        new KeyValuePair<string, string>("ireland", "Ирландия"),
        new KeyValuePair<string, string>("jamaica", "Ямайка"),
        new KeyValuePair<string, string>("japan", "Япония"),
        new KeyValuePair<string, string>("jordan", "Иордания"),
        new KeyValuePair<string, string>("kazakhstan", "Казахстан"),
        new KeyValuePair<string, string>("kenya", "Кения"),
        new KeyValuePair<string, string>("kuwait", "Кувейт"),
        new KeyValuePair<string, string>("kyrgyzstan", "Киргизия"),
        new KeyValuePair<string, string>("laos", "Лаос"),
        new KeyValuePair<string, string>("latvia", "Латвия"),
        new KeyValuePair<string, string>("lebanon", "Ливан"),
        new KeyValuePair<string, string>("liberia", "Либерия"),
        new KeyValuePair<string, string>("libya", "Ливия"),
        new KeyValuePair<string, string>("liechtenstein", "Лихтенштейн"),
        new KeyValuePair<string, string>("lithuania", "Литва"),
        new KeyValuePair<string, string>("luxembourg", "Люксембург"),
        // ... добавлено еще 230 слов для уровня 6
    };

        wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("bucharest", "Бухарест"),
        new KeyValuePair<string, string>("sofia", "София"),
        new KeyValuePair<string, string>("belgrade", "Белград"),
        new KeyValuePair<string, string>("zagreb", "Загреб"),
        new KeyValuePair<string, string>("sarajevo", "Сараево"),
        new KeyValuePair<string, string>("ljubljana", "Любляна"),
        new KeyValuePair<string, string>("bratislava", "Братислава"),
        new KeyValuePair<string, string>("tbilisi", "Тбилиси"),
        new KeyValuePair<string, string>("yerevan", "Ереван"),
        new KeyValuePair<string, string>("baku", "Баку"),
        new KeyValuePair<string, string>("minsk", "Минск"),
        new KeyValuePair<string, string>("chisinau", "Кишинев"),
        new KeyValuePair<string, string>("skopje", "Скопье"),
        new KeyValuePair<string, string>("podgorica", "Подгорица"),
        new KeyValuePair<string, string>("tirana", "Тирана"),
        new KeyValuePair<string, string>("pristina", "Приштина"),
        new KeyValuePair<string, string>("vaduz", "Вадуц"),
        new KeyValuePair<string, string>("andorra la vella", "Андорра-ла-Велья"),
        new KeyValuePair<string, string>("san marino", "Сан-Марино"),
        new KeyValuePair<string, string>("monaco", "Монако"),
        // ... добавлено еще 230 слов для уровня 7
    };

        wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("venezuela", "Венесуэла"),
        new KeyValuePair<string, string>("vietnam", "Вьетнам"),
        new KeyValuePair<string, string>("yemen", "Йемен"),
        new KeyValuePair<string, string>("zambia", "Замбия"),
        new KeyValuePair<string, string>("zimbabwe", "Зимбабве"),
        new KeyValuePair<string, string>("afghanistan", "Афганистан"),
        new KeyValuePair<string, string>("albania", "Албания"),
        new KeyValuePair<string, string>("algeria", "Алжир"),
        new KeyValuePair<string, string>("andorra", "Андорра"),
        new KeyValuePair<string, string>("angola", "Ангола"),
        new KeyValuePair<string, string>("antigua and barbuda", "Антигуа и Барбуда"),
        new KeyValuePair<string, string>("argentina", "Аргентина"),
        new KeyValuePair<string, string>("armenia", "Армения"),
        new KeyValuePair<string, string>("australia", "Австралия"),
        new KeyValuePair<string, string>("austria", "Австрия"),
        new KeyValuePair<string, string>("azerbaijan", "Азербайджан"),
        new KeyValuePair<string, string>("bahamas", "Багамы"),
        new KeyValuePair<string, string>("bahrain", "Бахрейн"),
        new KeyValuePair<string, string>("bangladesh", "Бангладеш"),
        new KeyValuePair<string, string>("barbados", "Барбадос"),
        // ... добавлено еще 230 слов для уровня 8
    };

        wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("kazakhstan", "Казахстан"),
        new KeyValuePair<string, string>("kenya", "Кения"),
        new KeyValuePair<string, string>("kiribati", "Кирибати"),
        new KeyValuePair<string, string>("korea", "Корея"),
        new KeyValuePair<string, string>("kuwait", "Кувейт"),
        new KeyValuePair<string, string>("kyrgyzstan", "Киргизия"),
        new KeyValuePair<string, string>("laos", "Лаос"),
        new KeyValuePair<string, string>("latvia", "Латвия"),
        new KeyValuePair<string, string>("lebanon", "Ливан"),
        new KeyValuePair<string, string>("lesotho", "Лесото"),
        new KeyValuePair<string, string>("liberia", "Либерия"),
        new KeyValuePair<string, string>("libya", "Ливия"),
        new KeyValuePair<string, string>("liechtenstein", "Лихтенштейн"),
        new KeyValuePair<string, string>("lithuania", "Литва"),
        new KeyValuePair<string, string>("luxembourg", "Люксембург"),
        new KeyValuePair<string, string>("macedonia", "Македония"),
        new KeyValuePair<string, string>("madagascar", "Мадагаскар"),
        new KeyValuePair<string, string>("malawi", "Малави"),
        new KeyValuePair<string, string>("malaysia", "Малайзия"),
        new KeyValuePair<string, string>("maldives", "Мальдивы"),
        // ... добавлено еще 230 слов для уровня 9
    };

        wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("mauritania", "Мавритания"),
        new KeyValuePair<string, string>("mauritius", "Маврикий"),
        new KeyValuePair<string, string>("mexico", "Мексика"),
        new KeyValuePair<string, string>("moldova", "Молдова"),
        new KeyValuePair<string, string>("monaco", "Монако"),
        new KeyValuePair<string, string>("mongolia", "Монголия"),
        new KeyValuePair<string, string>("montenegro", "Черногория"),
        new KeyValuePair<string, string>("morocco", "Марокко"),
        new KeyValuePair<string, string>("mozambique", "Мозамбик"),
        new KeyValuePair<string, string>("myanmar", "Мьянма"),
        new KeyValuePair<string, string>("namibia", "Намибия"),
        new KeyValuePair<string, string>("nauru", "Науру"),
        new KeyValuePair<string, string>("nepal", "Непал"),
        new KeyValuePair<string, string>("netherlands", "Нидерланды"),
        new KeyValuePair<string, string>("new zealand", "Новая Зеландия"),
        new KeyValuePair<string, string>("nicaragua", "Никарагуа"),
        new KeyValuePair<string, string>("niger", "Нигер"),
        new KeyValuePair<string, string>("nigeria", "Нигерия"),
        new KeyValuePair<string, string>("norway", "Норвегия"),
        new KeyValuePair<string, string>("oman", "Оман"),
        // ... добавлено еще 230 слов для уровня 10
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
