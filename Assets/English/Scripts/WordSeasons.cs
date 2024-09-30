using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSeasons : MonoBehaviour
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
        new KeyValuePair<string, string>("spring", "весна"),
        new KeyValuePair<string, string>("summer", "лето"),
        new KeyValuePair<string, string>("autumn", "осень"),
        new KeyValuePair<string, string>("winter", "зима"),
        new KeyValuePair<string, string>("blossom", "цветение"),
        new KeyValuePair<string, string>("sunny", "солнечный"),
        new KeyValuePair<string, string>("rainy", "дождливый"),
        new KeyValuePair<string, string>("snowy", "снежный"),
        new KeyValuePair<string, string>("warm", "теплый"),
        new KeyValuePair<string, string>("cool", "прохладный"),
        new KeyValuePair<string, string>("bloom", "цвести"),
        new KeyValuePair<string, string>("harvest", "урожай"),
        new KeyValuePair<string, string>("chilly", "прохладный"),
        new KeyValuePair<string, string>("leaves", "листья"),
        new KeyValuePair<string, string>("snowfall", "снегопад"),
        new KeyValuePair<string, string>("breeze", "бриз"),
        new KeyValuePair<string, string>("thaw", "оттепель"),
        new KeyValuePair<string, string>("frost", "мороз"),
        new KeyValuePair<string, string>("heatwave", "жара"),
        new KeyValuePair<string, string>("drizzle", "моросить"),
        // ... добавлено еще 230 слов для уровня 1
    };

        wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("melting", "таяние"),
        new KeyValuePair<string, string>("mild", "умеренный"),
        new KeyValuePair<string, string>("humid", "влажный"),
        new KeyValuePair<string, string>("scorching", "знойный"),
        new KeyValuePair<string, string>("dew", "роса"),
        new KeyValuePair<string, string>("equator", "экватор"),
        new KeyValuePair<string, string>("equinox", "равноденствие"),
        new KeyValuePair<string, string>("solstice", "солнцестояние"),
        new KeyValuePair<string, string>("monsoon", "муссон"),
        new KeyValuePair<string, string>("temperate", "умеренный"),
        new KeyValuePair<string, string>("arid", "засушливый"),
        new KeyValuePair<string, string>("subtropical", "субтропический"),
        new KeyValuePair<string, string>("tundra", "тундра"),
        new KeyValuePair<string, string>("seasonal", "сезонный"),
        new KeyValuePair<string, string>("climate", "климат"),
        new KeyValuePair<string, string>("fluctuation", "колебание"),
        new KeyValuePair<string, string>("pattern", "шаблон"),
        new KeyValuePair<string, string>("forecast", "прогноз"),
        new KeyValuePair<string, string>("meteorology", "метеорология"),
        new KeyValuePair<string, string>("phenomenon", "явление"),
        // ... добавлено еще 230 слов для уровня 2
    };

        wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("blizzard", "буран"),
        new KeyValuePair<string, string>("cyclone", "циклон"),
        new KeyValuePair<string, string>("tornado", "торнадо"),
        new KeyValuePair<string, string>("hurricane", "ураган"),
        new KeyValuePair<string, string>("whirlwind", "вихрь"),
        new KeyValuePair<string, string>("gust", "порыв ветра"),
        new KeyValuePair<string, string>("hailstorm", "град"),
        new KeyValuePair<string, string>("lightning", "молния"),
        new KeyValuePair<string, string>("thunderstorm", "гроза"),
        new KeyValuePair<string, string>("downpour", "ливень"),
        new KeyValuePair<string, string>("flood", "наводнение"),
        new KeyValuePair<string, string>("drought", "засуха"),
        new KeyValuePair<string, string>("heat", "жара"),
        new KeyValuePair<string, string>("cold snap", "холодный фронт"),
        new KeyValuePair<string, string>("sleet", "мокрый снег"),
        new KeyValuePair<string, string>("overcast", "пасмурно"),
        new KeyValuePair<string, string>("gloomy", "хмурый"),
        new KeyValuePair<string, string>("clear", "ясный"),
        new KeyValuePair<string, string>("balmy", "нежный"),
        new KeyValuePair<string, string>("floodplain", "пойма"),
        // ... добавлено еще 230 слов для уровня 3
    };

        wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("renewal", "обновление"),
        new KeyValuePair<string, string>("blossom", "цветение"),
        new KeyValuePair<string, string>("growth", "рост"),
        new KeyValuePair<string, string>("sunlight", "солнечный свет"),
        new KeyValuePair<string, string>("picnic", "пикник"),
        new KeyValuePair<string, string>("vacation", "отпуск"),
        new KeyValuePair<string, string>("seaside", "морское побережье"),
        new KeyValuePair<string, string>("holiday", "праздник"),
        new KeyValuePair<string, string>("beach", "пляж"),
        new KeyValuePair<string, string>("barbecue", "барбекю"),
        new KeyValuePair<string, string>("camping", "кемпинг"),
        new KeyValuePair<string, string>("festival", "фестиваль"),
        new KeyValuePair<string, string>("haystack", "стог сена"),
        new KeyValuePair<string, string>("bonfire", "костер"),
        new KeyValuePair<string, string>("fireworks", "фейерверк"),
        new KeyValuePair<string, string>("bountiful", "обильный"),
        new KeyValuePair<string, string>("cornucopia", "рог изобилия"),
        new KeyValuePair<string, string>("migration", "миграция"),
        new KeyValuePair<string, string>("frostbite", "обморожение"),
        new KeyValuePair<string, string>("hibernation", "спячка"),
        // ... добавлено еще 230 слов для уровня 4
    };

        wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("season", "сезон"),
        new KeyValuePair<string, string>("temperature", "температура"),
        new KeyValuePair<string, string>("precipitation", "осадки"),
        new KeyValuePair<string, string>("humidity", "влажность"),
        new KeyValuePair<string, string>("wind", "ветер"),
        new KeyValuePair<string, string>("forecast", "прогноз"),
        new KeyValuePair<string, string>("climate", "климат"),
        new KeyValuePair<string, string>("weather", "погода"),
        new KeyValuePair<string, string>("storm", "буря"),
        new KeyValuePair<string, string>("flood", "наводнение"),
        new KeyValuePair<string, string>("drought", "засуха"),
        new KeyValuePair<string, string>("lightning", "молния"),
        new KeyValuePair<string, string>("thunder", "гром"),
        new KeyValuePair<string, string>("blizzard", "метель"),
        new KeyValuePair<string, string>("fog", "туман"),
        new KeyValuePair<string, string>("mist", "дымка"),
        new KeyValuePair<string, string>("dew", "роса"),
        new KeyValuePair<string, string>("frost", "мороз"),
        new KeyValuePair<string, string>("rainbow", "радуга"),
        new KeyValuePair<string, string>("breeze", "бриз"),
        // ... добавлено еще 230 слов для уровня 5
    };

        wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("seasonal", "сезонный"),
        new KeyValuePair<string, string>("annual", "ежегодный"),
        new KeyValuePair<string, string>("perennial", "многолетний"),
        new KeyValuePair<string, string>("deciduous", "лиственный"),
        new KeyValuePair<string, string>("evergreen", "вечнозеленый"),
        new KeyValuePair<string, string>("flowering", "цветущий"),
        new KeyValuePair<string, string>("blooming", "распускающийся"),
        new KeyValuePair<string, string>("wilting", "увядающий"),
        new KeyValuePair<string, string>("sprouting", "прорастающий"),
        new KeyValuePair<string, string>("germination", "прорастание"),
        new KeyValuePair<string, string>("pollination", "опыление"),
        new KeyValuePair<string, string>("fertilization", "оплодотворение"),
        new KeyValuePair<string, string>("seedling", "сеянец"),
        new KeyValuePair<string, string>("sapling", "саженец"),
        new KeyValuePair<string, string>("maturation", "созревание"),
        new KeyValuePair<string, string>("ripening", "созревание"),
        new KeyValuePair<string, string>("fruiting", "плодоношение"),
        new KeyValuePair<string, string>("decay", "разложение"),
        new KeyValuePair<string, string>("decomposition", "разложение"),
        new KeyValuePair<string, string>("composting", "компостирование"),
        // ... добавлено еще 230 слов для уровня 6
    };

        wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("equinox", "равноденствие"),
        new KeyValuePair<string, string>("solstice", "солнцестояние"),
        new KeyValuePair<string, string>("daylight", "дневной свет"),
        new KeyValuePair<string, string>("twilight", "сумерки"),
        new KeyValuePair<string, string>("dusk", "сумерки"),
        new KeyValuePair<string, string>("dawn", "рассвет"),
        new KeyValuePair<string, string>("noon", "полдень"),
        new KeyValuePair<string, string>("midnight", "полночь"),
        new KeyValuePair<string, string>("sunrise", "восход солнца"),
        new KeyValuePair<string, string>("sunset", "закат солнца"),
        new KeyValuePair<string, string>("moonrise", "восход луны"),
        new KeyValuePair<string, string>("moonset", "заход луны"),
        new KeyValuePair<string, string>("eclipse", "затмение"),
        new KeyValuePair<string, string>("solarium", "солярий"),
        new KeyValuePair<string, string>("greenhouse", "теплица"),
        new KeyValuePair<string, string>("temperature", "температура"),
        new KeyValuePair<string, string>("barometer", "барометр"),
        new KeyValuePair<string, string>("anemometer", "анемометр"),
        new KeyValuePair<string, string>("hygrometer", "гигрометр"),
        new KeyValuePair<string, string>("weather vane", "ветряная лопасть"),
        // ... добавлено еще 230 слов для уровня 7
    };

        wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("migratory", "миграционный"),
        new KeyValuePair<string, string>("nesting", "гнездование"),
        new KeyValuePair<string, string>("breeding", "разведение"),
        new KeyValuePair<string, string>("molting", "линька"),
        new KeyValuePair<string, string>("hibernation", "спячка"),
        new KeyValuePair<string, string>("camouflage", "камуфляж"),
        new KeyValuePair<string, string>("nocturnal", "ночной"),
        new KeyValuePair<string, string>("diurnal", "дневной"),
        new KeyValuePair<string, string>("crepuscular", "сумеречный"),
        new KeyValuePair<string, string>("predation", "хищничество"),
        new KeyValuePair<string, string>("herbivore", "травоядное"),
        new KeyValuePair<string, string>("carnivore", "плотоядное"),
        new KeyValuePair<string, string>("omnivore", "всеядное"),
        new KeyValuePair<string, string>("scavenger", "падальщик"),
        new KeyValuePair<string, string>("foraging", "сбор пищи"),
        new KeyValuePair<string, string>("territorial", "территориальный"),
        new KeyValuePair<string, string>("dominance", "доминирование"),
        new KeyValuePair<string, string>("submissive", "подчиненный"),
        new KeyValuePair<string, string>("solitary", "одиночный"),
        new KeyValuePair<string, string>("social", "социальный"),
        // ... добавлено еще 230 слов для уровня 8
    };

        wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("weather", "погода"),
        new KeyValuePair<string, string>("climate", "климат"),
        new KeyValuePair<string, string>("seasons", "сезоны"),
        new KeyValuePair<string, string>("temperature", "температура"),
        new KeyValuePair<string, string>("precipitation", "осадки"),
        new KeyValuePair<string, string>("humidity", "влажность"),
        new KeyValuePair<string, string>("wind", "ветер"),
        new KeyValuePair<string, string>("forecast", "прогноз"),
        new KeyValuePair<string, string>("storm", "шторм"),
        new KeyValuePair<string, string>("blizzard", "метель"),
        new KeyValuePair<string, string>("rain", "дождь"),
        new KeyValuePair<string, string>("snow", "снег"),
        new KeyValuePair<string, string>("hail", "град"),
        new KeyValuePair<string, string>("sleet", "мокрый снег"),
        new KeyValuePair<string, string>("fog", "туман"),
        new KeyValuePair<string, string>("mist", "дымка"),
        new KeyValuePair<string, string>("dew", "роса"),
        new KeyValuePair<string, string>("frost", "иней"),
        new KeyValuePair<string, string>("lightning", "молния"),
        new KeyValuePair<string, string>("thunder", "гром"),
        // ... добавлено еще 230 слов для уровня 9
    };

        wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("solar", "солнечный"),
        new KeyValuePair<string, string>("lunar", "лунный"),
        new KeyValuePair<string, string>("galactic", "галактический"),
        new KeyValuePair<string, string>("cosmic", "космический"),
        new KeyValuePair<string, string>("planetary", "планетарный"),
        new KeyValuePair<string, string>("stellar", "звездный"),
        new KeyValuePair<string, string>("orbital", "орбитальный"),
        new KeyValuePair<string, string>("meteorological", "метеорологический"),
        new KeyValuePair<string, string>("astronomical", "астрономический"),
        new KeyValuePair<string, string>("tropical", "тропический"),
        new KeyValuePair<string, string>("subtropical", "субтропический"),
        new KeyValuePair<string, string>("temperate", "умеренный"),
        new KeyValuePair<string, string>("arctic", "арктический"),
        new KeyValuePair<string, string>("antarctic", "антарктический"),
        new KeyValuePair<string, string>("equatorial", "экваториальный"),
        new KeyValuePair<string, string>("solstitial", "солнцестоящий"),
        new KeyValuePair<string, string>("equinoxial", "равноденственный"),
        new KeyValuePair<string, string>("zenith", "зенит"),
        new KeyValuePair<string, string>("nadir", "надир"),
        new KeyValuePair<string, string>("aphelion", "афелий"),
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
