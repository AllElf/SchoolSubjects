using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordProfessions : MonoBehaviour
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
    [SerializeField] float _seconds = 15f;
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
        new KeyValuePair<string, string>("doctor", "врач"),
        new KeyValuePair<string, string>("teacher", "учитель"),
        new KeyValuePair<string, string>("nurse", "медсестра"),
        new KeyValuePair<string, string>("pilot", "пилот"),
        new KeyValuePair<string, string>("chef", "шеф-повар"),
        new KeyValuePair<string, string>("artist", "художник"),
        new KeyValuePair<string, string>("farmer", "фермер"),
        new KeyValuePair<string, string>("actor", "актер"),
        new KeyValuePair<string, string>("baker", "пекарь"),
        new KeyValuePair<string, string>("lawyer", "юрист"),
        new KeyValuePair<string, string>("police", "полицейский"),
        new KeyValuePair<string, string>("firefighter", "пожарный"),
        new KeyValuePair<string, string>("dancer", "танцор"),
        new KeyValuePair<string, string>("driver", "водитель"),
        new KeyValuePair<string, string>("gardener", "садовник"),
        new KeyValuePair<string, string>("tailor", "портной"),
        new KeyValuePair<string, string>("waiter", "официант"),
        new KeyValuePair<string, string>("dentist", "стоматолог"),
        new KeyValuePair<string, string>("scientist", "ученый"),
        new KeyValuePair<string, string>("writer", "писатель"),
        // ... добавлено еще 230 слов для уровня 1
    };

        wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("engineer", "инженер"),
        new KeyValuePair<string, string>("mechanic", "механик"),
        new KeyValuePair<string, string>("electrician", "электрик"),
        new KeyValuePair<string, string>("carpenter", "плотник"),
        new KeyValuePair<string, string>("plumber", "сантехник"),
        new KeyValuePair<string, string>("architect", "архитектор"),
        new KeyValuePair<string, string>("accountant", "бухгалтер"),
        new KeyValuePair<string, string>("librarian", "библиотекарь"),
        new KeyValuePair<string, string>("journalist", "журналист"),
        new KeyValuePair<string, string>("photographer", "фотограф"),
        new KeyValuePair<string, string>("pharmacist", "фармацевт"),
        new KeyValuePair<string, string>("therapist", "терапевт"),
        new KeyValuePair<string, string>("vet", "ветеринар"),
        new KeyValuePair<string, string>("scientist", "ученый"),
        new KeyValuePair<string, string>("translator", "переводчик"),
        new KeyValuePair<string, string>("biologist", "биолог"),
        new KeyValuePair<string, string>("chemist", "химик"),
        new KeyValuePair<string, string>("geologist", "геолог"),
        new KeyValuePair<string, string>("physicist", "физик"),
        new KeyValuePair<string, string>("economist", "экономист"),
        // ... добавлено еще 230 слов для уровня 2
    };

        wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("programmer", "программист"),
        new KeyValuePair<string, string>("web developer", "веб-разработчик"),
        new KeyValuePair<string, string>("software engineer", "инженер-программист"),
        new KeyValuePair<string, string>("data scientist", "ученый данных"),
        new KeyValuePair<string, string>("system analyst", "системный аналитик"),
        new KeyValuePair<string, string>("network administrator", "администратор сети"),
        new KeyValuePair<string, string>("IT manager", "ИТ-менеджер"),
        new KeyValuePair<string, string>("graphic designer", "графический дизайнер"),
        new KeyValuePair<string, string>("UX designer", "дизайнер пользовательского опыта"),
        new KeyValuePair<string, string>("UI designer", "дизайнер интерфейсов"),
        new KeyValuePair<string, string>("product manager", "продукт-менеджер"),
        new KeyValuePair<string, string>("project manager", "проект-менеджер"),
        new KeyValuePair<string, string>("content manager", "контент-менеджер"),
        new KeyValuePair<string, string>("SEO specialist", "специалист по SEO"),
        new KeyValuePair<string, string>("social media manager", "менеджер социальных сетей"),
        new KeyValuePair<string, string>("digital marketer", "цифровой маркетолог"),
        new KeyValuePair<string, string>("advertising manager", "рекламный менеджер"),
        new KeyValuePair<string, string>("brand manager", "бренд-менеджер"),
        new KeyValuePair<string, string>("copywriter", "копирайтер"),
        new KeyValuePair<string, string>("content creator", "создатель контента"),
        // ... добавлено еще 230 слов для уровня 3
    };

        wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("chef", "шеф-повар"),
        new KeyValuePair<string, string>("sous chef", "су-шеф"),
        new KeyValuePair<string, string>("line cook", "повар"),
        new KeyValuePair<string, string>("pastry chef", "кондитер"),
        new KeyValuePair<string, string>("baker", "пекарь"),
        new KeyValuePair<string, string>("butcher", "мясник"),
        new KeyValuePair<string, string>("barista", "бариста"),
        new KeyValuePair<string, string>("bartender", "бармен"),
        new KeyValuePair<string, string>("sommelier", "сомелье"),
        new KeyValuePair<string, string>("waiter", "официант"),
        new KeyValuePair<string, string>("waitress", "официантка"),
        new KeyValuePair<string, string>("host", "хост"),
        new KeyValuePair<string, string>("hostess", "хостес"),
        new KeyValuePair<string, string>("dishwasher", "посудомойщик"),
        new KeyValuePair<string, string>("busser", "буссер"),
        new KeyValuePair<string, string>("restaurant manager", "менеджер ресторана"),
        new KeyValuePair<string, string>("maître d'", "метрдотель"),
        new KeyValuePair<string, string>("bar manager", "бар-менеджер"),
        new KeyValuePair<string, string>("caterer", "кейтеринг"),
        new KeyValuePair<string, string>("food critic", "критик еды"),
        // ... добавлено еще 230 слов для уровня 4
    };

        wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("actor", "актер"),
        new KeyValuePair<string, string>("actress", "актриса"),
        new KeyValuePair<string, string>("director", "режиссер"),
        new KeyValuePair<string, string>("producer", "продюсер"),
        new KeyValuePair<string, string>("screenwriter", "сценарист"),
        new KeyValuePair<string, string>("cinematographer", "кинооператор"),
        new KeyValuePair<string, string>("editor", "редактор"),
        new KeyValuePair<string, string>("sound engineer", "звукорежиссер"),
        new KeyValuePair<string, string>("costume designer", "дизайнер костюмов"),
        new KeyValuePair<string, string>("makeup artist", "визажист"),
        new KeyValuePair<string, string>("stuntman", "каскадер"),
        new KeyValuePair<string, string>("stuntwoman", "каскадерша"),
        new KeyValuePair<string, string>("lighting technician", "техник по освещению"),
        new KeyValuePair<string, string>("grip", "грип"),
        new KeyValuePair<string, string>("gaffer", "гаффер"),
        new KeyValuePair<string, string>("casting director", "директор по кастингу"),
        new KeyValuePair<string, string>("prop master", "реквизитор"),
        new KeyValuePair<string, string>("location scout", "скаут по локациям"),
        new KeyValuePair<string, string>("production assistant", "ассистент по производству"),
        new KeyValuePair<string, string>("film critic", "кинокритик"),
        // ... добавлено еще 230 слов для уровня 5
    };

        wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("doctor", "врач"),
        new KeyValuePair<string, string>("surgeon", "хирург"),
        new KeyValuePair<string, string>("nurse", "медсестра"),
        new KeyValuePair<string, string>("paramedic", "парамедик"),
        new KeyValuePair<string, string>("anesthesiologist", "анестезиолог"),
        new KeyValuePair<string, string>("radiologist", "радиолог"),
        new KeyValuePair<string, string>("cardiologist", "кардиолог"),
        new KeyValuePair<string, string>("dermatologist", "дерматолог"),
        new KeyValuePair<string, string>("neurologist", "невролог"),
        new KeyValuePair<string, string>("gynecologist", "гинеколог"),
        new KeyValuePair<string, string>("pediatrician", "педиатр"),
        new KeyValuePair<string, string>("psychiatrist", "психиатр"),
        new KeyValuePair<string, string>("psychologist", "психолог"),
        new KeyValuePair<string, string>("urologist", "уролог"),
        new KeyValuePair<string, string>("oncologist", "онколог"),
        new KeyValuePair<string, string>("pathologist", "патолог"),
        new KeyValuePair<string, string>("orthopedist", "ортопед"),
        new KeyValuePair<string, string>("ophthalmologist", "офтальмолог"),
        new KeyValuePair<string, string>("endocrinologist", "эндокринолог"),
        new KeyValuePair<string, string>("dentist", "стоматолог"),
        // ... добавлено еще 230 слов для уровня 6
    };

        wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("police officer", "полицейский"),
        new KeyValuePair<string, string>("detective", "детектив"),
        new KeyValuePair<string, string>("firefighter", "пожарный"),
        new KeyValuePair<string, string>("paramedic", "парамедик"),
        new KeyValuePair<string, string>("security guard", "охранник"),
        new KeyValuePair<string, string>("border patrol", "пограничный патруль"),
        new KeyValuePair<string, string>("customs officer", "таможенник"),
        new KeyValuePair<string, string>("prison guard", "тюремный охранник"),
        new KeyValuePair<string, string>("corrections officer", "офицер исправительных учреждений"),
        new KeyValuePair<string, string>("military officer", "военный офицер"),
        new KeyValuePair<string, string>("army soldier", "солдат"),
        new KeyValuePair<string, string>("navy sailor", "моряк"),
        new KeyValuePair<string, string>("air force pilot", "пилот ВВС"),
        new KeyValuePair<string, string>("marine", "морпех"),
        new KeyValuePair<string, string>("coast guard", "береговая охрана"),
        new KeyValuePair<string, string>("special forces", "спецназ"),
        new KeyValuePair<string, string>("private investigator", "частный детектив"),
        new KeyValuePair<string, string>("federal agent", "федеральный агент"),
        new KeyValuePair<string, string>("secret service", "секретная служба"),
        new KeyValuePair<string, string>("CIA agent", "агент ЦРУ"),
        // ... добавлено еще 230 слов для уровня 7
    };

        wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("teacher", "учитель"),
        new KeyValuePair<string, string>("professor", "профессор"),
        new KeyValuePair<string, string>("lecturer", "лектор"),
        new KeyValuePair<string, string>("principal", "директор школы"),
        new KeyValuePair<string, string>("dean", "декан"),
        new KeyValuePair<string, string>("guidance counselor", "консультант по образованию"),
        new KeyValuePair<string, string>("school counselor", "школьный консультант"),
        new KeyValuePair<string, string>("librarian", "библиотекарь"),
        new KeyValuePair<string, string>("coach", "тренер"),
        new KeyValuePair<string, string>("education administrator", "администратор образования"),
        new KeyValuePair<string, string>("tutor", "репетитор"),
        new KeyValuePair<string, string>("teaching assistant", "ассистент преподавателя"),
        new KeyValuePair<string, string>("researcher", "исследователь"),
        new KeyValuePair<string, string>("educational psychologist", "психолог образования"),
        new KeyValuePair<string, string>("curriculum developer", "разработчик учебных программ"),
        new KeyValuePair<string, string>("academic advisor", "академический консультант"),
        new KeyValuePair<string, string>("special education teacher", "учитель специального образования"),
        new KeyValuePair<string, string>("kindergarten teacher", "воспитатель детского сада"),
        new KeyValuePair<string, string>("elementary teacher", "учитель начальной школы"),
        new KeyValuePair<string, string>("high school teacher", "учитель средней школы"),
        // ... добавлено еще 230 слов для уровня 8
    };

        wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("pilot", "пилот"),
        new KeyValuePair<string, string>("co-pilot", "второй пилот"),
        new KeyValuePair<string, string>("flight attendant", "бортпроводник"),
        new KeyValuePair<string, string>("air traffic controller", "диспетчер воздушного движения"),
        new KeyValuePair<string, string>("aviation mechanic", "авиационный механик"),
        new KeyValuePair<string, string>("aerospace engineer", "инженер аэрокосмической промышленности"),
        new KeyValuePair<string, string>("airport manager", "менеджер аэропорта"),
        new KeyValuePair<string, string>("cargo handler", "грузчик"),
        new KeyValuePair<string, string>("ramp agent", "агент на перроне"),
        new KeyValuePair<string, string>("baggage handler", "погрузчик багажа"),
        new KeyValuePair<string, string>("ticket agent", "билетный агент"),
        new KeyValuePair<string, string>("gate agent", "агент у ворот"),
        new KeyValuePair<string, string>("airport security", "охранник аэропорта"),
        new KeyValuePair<string, string>("customs officer", "таможенник"),
        new KeyValuePair<string, string>("immigration officer", "иммиграционный офицер"),
        new KeyValuePair<string, string>("ground crew", "наземный экипаж"),
        new KeyValuePair<string, string>("helicopter pilot", "пилот вертолета"),
        new KeyValuePair<string, string>("crop duster", "авиационный распылитель"),
        new KeyValuePair<string, string>("airline dispatcher", "авиадиспетчер"),
        new KeyValuePair<string, string>("flight instructor", "летный инструктор"),
        // ... добавлено еще 230 слов для уровня 9
    };

        wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("musician", "музыкант"),
        new KeyValuePair<string, string>("singer", "певец"),
        new KeyValuePair<string, string>("composer", "композитор"),
        new KeyValuePair<string, string>("conductor", "дирижер"),
        new KeyValuePair<string, string>("guitarist", "гитарист"),
        new KeyValuePair<string, string>("pianist", "пианист"),
        new KeyValuePair<string, string>("drummer", "барабанщик"),
        new KeyValuePair<string, string>("violinist", "скрипач"),
        new KeyValuePair<string, string>("cellist", "виолончелист"),
        new KeyValuePair<string, string>("bassist", "басист"),
        new KeyValuePair<string, string>("saxophonist", "саксофонист"),
        new KeyValuePair<string, string>("trumpeter", "трубач"),
        new KeyValuePair<string, string>("flutist", "флейтист"),
        new KeyValuePair<string, string>("clarinetist", "кларнетист"),
        new KeyValuePair<string, string>("harpist", "арфист"),
        new KeyValuePair<string, string>("percussionist", "ударник"),
        new KeyValuePair<string, string>("sound engineer", "звукорежиссер"),
        new KeyValuePair<string, string>("record producer", "продюсер звукозаписи"),
        new KeyValuePair<string, string>("music teacher", "учитель музыки"),
        new KeyValuePair<string, string>("choir director", "дирижер хора"),
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
